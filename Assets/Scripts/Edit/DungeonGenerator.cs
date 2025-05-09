using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ���� ���ʷ������Դϴ�. ������ Room�� �������� ������ �̷��� �����Դϴ�.
/// ���� ������Ʈ Ǯ���� ����Ǿ� ���� ������, �̴� ��Ÿ���� �ƴ� ������ �󿡼�
/// Ŀ���� ���� ���� �׽�Ʈ�� �����ϰ� �ֱ� �����Դϴ�.
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
    /// ��� ������ �����ϰ�, ù ��° ������ Ȱ��ȭ�մϴ�.
    /// �ش� �żҵ�� �׽�Ʈ�� ���� �����մϴ�.    
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

        // ���� Ȱ��ȭ
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
    /// ��, ����, �������� ���� �� ������Ʈ ����
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
    /// ������ �ν��Ͻ�ȭ�մϴ�.
    /// </summary>
    private void RoomBuild(DungeonDataSO model)
    {
        DungeonController dc = CreateHolder(model);
        dic.Clear();

        // �� ����
        foreach (var roomData in model.rooms)
        {
            var objRoom = Instantiate(roomPrefab);
            Room roomComp = objRoom.GetComponent<Room>();
            dc.rooms.Add(roomComp);
            objRoom.transform.SetParent(roomHolder.transform);

            roomComp.Init(roomData);
            dic.Add(roomComp.Guid, roomComp);
        }

        // �� ���� ��Ż �¾�
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
    /// ������ �´� ���� �����ϰ�, �����۰� ���͸� �����մϴ�.
    /// </summary>
    public void BuildToLevel(int level)
    {
        RoomBuild(dungeonDatas[level - 1]);
        CreateMonsterAndItem(level);
    }


    /// <summary>
    /// ������ �����۰� ���͸� �ν��Ͻ�ȭ�մϴ�.
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
    /// ������ ���� Ȯ���� �Ѹ��մϴ�.
    /// </summary>
    private int RollLevel(int chance1, int chance2, int chance3)
    {
        int roll = Random.Range(0, 100);
        if (roll < chance1) return 1;
        else if (roll < chance1 + chance2) return 2;
        else return 3;
    }
}