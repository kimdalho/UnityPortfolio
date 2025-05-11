using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using static Unity.Burst.Intrinsics.Arm;
/// <summary>
/// ���� ���ʷ������Դϴ�. ������ Room�� �������� ������ �̷��� �����Դϴ�.
/// ���� ������Ʈ Ǯ���� ����Ǿ� ���� ������, �̴� ��Ÿ���� �ƴ� ������ �󿡼�
/// Ŀ���� ���� ���� �׽�Ʈ�� �����ϰ� �ֱ� �����Դϴ�.
/// </summary>
public class DungeonGenerator : MonoBehaviour
{ 
    [SerializeField]
    private GameObject dungeonControllerPrefab;
    [Header("ScriptablObject")]
    [SerializeField]
    private DungeonConfigSO dungeonConfig;
    [SerializeField]
    public List<DungeonCraftDataSO> dungeonDatas;


    private Dictionary<string, Room> dic = new Dictionary<string, Room>();
    
    private GameObject dungeonController;

    private GameObject itemHolder;
    private GameObject monsterHolder;
    private List<GameObject> dungeons = new List<GameObject>();
    
    private readonly float BOSS_SCALE = 2.3f;

    private static readonly Dictionary<eDirection, eDirection> flipMap = new Dictionary<eDirection, eDirection>
    {
        { eDirection.Left, eDirection.Right },
        { eDirection.Right, eDirection.Left },
        { eDirection.Top, eDirection.Bottom },
        { eDirection.Bottom, eDirection.Top }
    };

#region EditCode
#if UNITY_EDITOR

    public void BuildPoolingRooms()
    {
        GameObject pooling = new GameObject();
        ResourceManager.Instance.CreateRoomPool(pooling.transform);
    }

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
        if (level - 1 >= dungeonDatas.Count)
        {
            Debug.Log("Ŭ����");
            return null;
        }

        BuildToLevel(level);
        return dungeons[level - 1].GetComponent<DungeonController>();
    }

    /// <summary>
    /// ��, ����, �������� ���� �� ������Ʈ ����
    /// </summary>
    private DungeonController CreateHolder(DungeonCraftDataSO model)
    {

        dungeonController = Instantiate(dungeonControllerPrefab);
        dungeonController.name = $"Dungeon {model.name}";
        dungeons.Add(dungeonController);   
        monsterHolder = CreateChildHolder("MonsterHolder", model.name, dungeonController.transform);
        itemHolder = CreateChildHolder("ItemHolder", model.name, dungeonController.transform);

        return dungeonController.GetComponent<DungeonController>();

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
    private void RoomBuild(DungeonCraftDataSO model)
    {
        DungeonController dc = CreateHolder(model);
        dic.Clear();

        // �� ����
        foreach (var dungeonData in model.dungeonDatas)
        {
            Room roomComp = ResourceManager.Instance.GetRoom(dungeonData.roomType);
            dc.rooms.Add(roomComp);
            roomComp.transform.position = new Vector3(dungeonData.position.x, 0, dungeonData.position.y);           
            
            dic.Add(dungeonData.guid, roomComp);
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
        EquipmentItem item = room.grid.CreateItem(itemHolder.transform, tier);
        dc.items.Add(item);
    }

    private void CreateMonstersInRoom(Room room, DungeonRoomConfig config, int count, DungeonController dc)
    {
        for (int i = 0; i < count; i++)
        {
            int level = RollLevel(config.monsterLevel1Chance, config.monsterLevel2Chance, config.monsterLevel3Chance);
            Monster monster = ResourceManager.Instance.CreateMonsterToLevel(level,monsterHolder.transform);
            monster.SetNode(room);                     
            monster.OnDeath += room.OnMonsterDeath;
            monster.ResetPos();
            room.roomMonsters.Add(monster);
            dc.monsters.Add(monster);
        }
    }

    private void CreateBossInRoom(Room room, DungeonRoomConfig config, DungeonController dc)
    {
        int level = RollLevel(config.monsterLevel1Chance, config.monsterLevel2Chance, config.monsterLevel3Chance);
        Monster boss = ResourceManager.Instance.CreateMonsterToLevel(level, monsterHolder.transform);
        boss.SetNode(room);
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