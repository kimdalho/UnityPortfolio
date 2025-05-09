using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 던전 제너레이터입니다. 던전은 Room의 집합으로 구성된 미로형 구조입니다.
/// 현재 오브젝트 풀링은 적용되어 있지 않으며, 이는 런타임이 아닌 에디터 상에서
/// 커스텀 툴을 통해 테스트를 진행하고 있기 때문입니다.
/// </summary>
public class DungeonGenerator : MonoBehaviour
{
    [Header("Prefab")]
    [SerializeField]
    private GameObject roomPrefab;
    [SerializeField]
    private GameObject dungeonControllerPrefab;
    [Header("ScriptablObject")]
    [SerializeField]
    private DungeonConfigSO dungeonConfig;
    [SerializeField]
    public List<DungeonDataSO> dungeonDatas;
    [SerializeField]
    private MonsterDataSO[] monsters;


    private Dictionary<string, Room> dic = new Dictionary<string, Room>();
    private Dictionary<int, List<MonsterDataSO>> monsterLvDatas;
    
    private GameObject dungeonController;

    private GameObject itemHolder;
    private GameObject roomHolder;
    private GameObject monsterHolder;
    private List<GameObject> dungeons = new List<GameObject>();
    
    private readonly float BOSS_SCALE = 2.3f;

    private readonly int MIN_LEVEL = 1;
    private readonly int MAX_LEVEL = 3;

    private static readonly Dictionary<eDirection, eDirection> flipMap = new Dictionary<eDirection, eDirection>
    {
        { eDirection.Left, eDirection.Right },
        { eDirection.Right, eDirection.Left },
        { eDirection.Top, eDirection.Bottom },
        { eDirection.Bottom, eDirection.Top }
    };

#region EditCode
#if UNITY_EDITOR
    /// <summary>
    /// 모든 던전을 생성하고, 첫 번째 던전만 활성화합니다.
    /// 해당 매소드는 테스트를 위해 존재합니다.    
    /// </summary>
    public void BuildAllDungeonsInEditor()
    {
        dungeons.Clear();
        int i = 0;
        foreach (var roomData in dungeonDatas)
        {
            RoomBuild(roomData);
            CreateMonsterAndItem(i + 1);
            i++;
        }

        // 던전 활성화
        i = 0;
        foreach (var dungeon in dungeons)
        {
            if(dungeon == null)
            {
                Debug.LogError("DungeonGenerator -> dungeon Not Found");
            }

            if (i > 0)
                dungeon.SetActive(false);
            i++;
        }        
    }

#endif
#endregion

    public DungeonController Build(int level)
    {
        BuildToLevel(level);
        return dungeons[level - 1].GetComponent<DungeonController>();
    }

    /// <summary>
    /// 룸, 몬스터, 아이템을 담을 빈 오브젝트 생성
    /// </summary>
    private DungeonController CreateHolder(DungeonDataSO model)
    {
        EnsureMonsterLevelDataInitialized();

        dungeonController = Instantiate(dungeonControllerPrefab);
        dungeonController.name = $"Dungeon {model.name}";
        dungeons.Add(dungeonController);

        roomHolder = CreateChildHolder("RoomHolder", model.name, dungeonController.transform);
        monsterHolder = CreateChildHolder("MonsterHolder", model.name, dungeonController.transform);
        itemHolder = CreateChildHolder("ItemHolder", model.name, dungeonController.transform);

        return dungeonController.GetComponent<DungeonController>();

    }

    private void EnsureMonsterLevelDataInitialized()
    {
        if (monsterLvDatas != null) return;

        monsterLvDatas = new Dictionary<int, List<MonsterDataSO>>();

        foreach (var data in monsters)
        {
            if (!monsterLvDatas.TryGetValue(data.level, out var list))
            {
                list = new List<MonsterDataSO>();
                monsterLvDatas[data.level] = list;
            }

            list.Add(data);
        }
    }

    private GameObject CreateChildHolder(string prefix, string modelName, Transform parent)
    {
        GameObject holder = new GameObject($"{prefix} {modelName}");
        holder.transform.SetParent(parent);
        return holder;
    }

    /// <summary>
    /// 던전을 인스턴스화합니다.
    /// </summary>
    private void RoomBuild(DungeonDataSO model)
    {
        DungeonController dc = CreateHolder(model);
        dic.Clear();

        // 룸 생성
        foreach (var roomData in model.rooms)
        {
            var objRoom = Instantiate(roomPrefab);
            Room roomComp = objRoom.GetComponent<Room>();
            dc.rooms.Add(roomComp);
            objRoom.transform.SetParent(roomHolder.transform);

            roomComp.Init(roomData);
            dic.Add(roomComp.Guid, roomComp);
        }

        // 룸 간의 포탈 셋업
        foreach (var linkData in model.links)
        {
            var input = dic[linkData.fromNodeGUID];
            string outputDirection = linkData.direction.Split(' ')[1].Trim();
            eDirection direction = (eDirection)System.Enum.Parse(typeof(eDirection), outputDirection);

            var portal = input.keyValuePairs[direction];
            eDirection flipDirection = GetFlipDirection(direction);

            portal.toNodeGUID = linkData.toNodeGUID;
            portal.toNextRoom = dic[linkData.toNodeGUID];
            portal.toNextSpawnPoint = dic[linkData.toNodeGUID].keyValuePairs[flipDirection].SpawnPoint;
            input.enablePortal.Add(portal);
            dc.portals.Add(portal);
        }
    }

    private eDirection GetFlipDirection(eDirection direction)
    {
        return flipMap.TryGetValue(direction, out var flipped) ? flipped : direction;
    }

    /// <summary>
    /// 계층에 맞는 룸을 생성하고, 아이템과 몬스터를 생성합니다.
    /// </summary>
    public void BuildToLevel(int level)
    {
        RoomBuild(dungeonDatas[level - 1]);
        CreateMonsterAndItem(level);
    }


    /// <summary>
    /// 던전에 아이템과 몬스터를 인스턴스화합니다.
    /// </summary>
    private void CreateMonsterAndItem(int level)
    {
        DungeonRoomConfig config = dungeonConfig.GetConfigByLevel(level);
        DungeonController componenet = dungeonController.GetComponent<DungeonController>();
        int itemCount = Random.Range(config.minItems, config.maxItems + 1);
        int monsterCount = Random.Range(config.minMonsters, config.maxMonsters + 1);

        foreach (var room in dic.Values)
        {
            switch (room.roomType)
            {
                case eRoomType.Item:
                    CreateItemInRoom(room, config, componenet);
                    break;

                case eRoomType.Monster:
                    CreateMonstersInRoom(room, config, monsterCount, componenet);
                    break;

                case eRoomType.Boss:
                    CreateBossInRoom(room, config, componenet);
                    break;

                case eRoomType.Start:
                    CreateStartRoomItem(room);
                    break;
            }
        }
    }

    private void CreateItemInRoom(Room room, DungeonRoomConfig config, DungeonController dc)
    {
        int tier = RollLevel(config.itemTier1Chance, config.itemTier2Chance, config.itemTier3Chance);
        GameObject item = room.grid.CreateItem(itemHolder.transform, tier);
        dc.items.Add(item);
    }

    private void CreateMonstersInRoom(Room room, DungeonRoomConfig config, int count, DungeonController dc)
    {
        for (int i = 0; i < count; i++)
        {
            MonsterDataSO data = GetRandomMonsterData(config);
            Monster monster = InstantiateMonster(room, data,$"Monster {i}");
            dc.monsters.Add(monster);
            room.roomMonsters.Add(monster);

            monster.OnDeath += room.OnMonsterDeath;
            monster.ResetPos();
        }
    }

    private void CreateBossInRoom(Room room, DungeonRoomConfig config, DungeonController dc)
    {
        MonsterDataSO data = GetRandomMonsterData(config);
        Monster boss = InstantiateMonster(room, data, "Boss", BOSS_SCALE);
        dc.monsters.Add(boss);
        room.roomMonsters.Add(boss);

        boss.OnDeath += room.OnMonsterDeath;
        boss.ResetPos();
    }

    private void CreateStartRoomItem(Room room)
    {
        //int weaponIndex = Random.Range(0, 3);
        int rifleIndex = 0;
        var node = room.grid.GetFirstGridNode();
        ResourceManager.Instance.CreateWeaponItemToIndex(rifleIndex, node.transform);
    }

    private Monster InstantiateMonster(Room room, MonsterDataSO data, string namePrefix, float scaleMultiplier = 1.0f)
    {
        Monster monster = room.grid.CreateMonster(monsterHolder.transform);
        monster.name = $"{namePrefix} {monster.name}";
        monster.transform.localScale *= scaleMultiplier;
        monster.SetData(data);
        return monster;
    }

    private MonsterDataSO GetRandomMonsterData(DungeonRoomConfig config)
    {
        int level = RollLevel(config.monsterLevel1Chance, config.monsterLevel2Chance, config.monsterLevel3Chance);
        int index = Random.Range(MIN_LEVEL, MAX_LEVEL);
        return monsterLvDatas[level][index];
    }

    /// <summary>
    /// 레벨에 따른 확률을 롤링합니다.
    /// </summary>
    private int RollLevel(int chance1, int chance2, int chance3)
    {
        int roll = Random.Range(0, 100);
        if (roll < chance1) return 1;
        else if (roll < chance1 + chance2) return 2;
        else return 3;
    }
}