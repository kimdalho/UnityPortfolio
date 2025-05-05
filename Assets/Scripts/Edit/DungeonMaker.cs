using System.Collections.Generic;
using UnityEngine;




public class DungeonMaker : MonoBehaviour
{
    public DungeonData roomData;
    public GameObject roomprefab;
    public int offsetX;
    public int offsetY;

    public Dictionary<string, Room> dic = new Dictionary<string, Room>();

    //몬스터 데이터
    public MonsterLevelDataSO[] monsterLevel;

    public Dictionary<int, List<MonsterLevelDataSO>> monsterLvDatas;

    //던전 데이터
    public DungeonConfigSO dungeonConfig;
    public int currentDungeonLevel = 5;
    //홀더
        
    public GameObject dungeonControllerPrfab;
    public GameObject dungeonController;

    private GameObject itemHolder;
    private GameObject roomHolder;
    private GameObject monsterHolder;
    List<GameObject> dungeons = new List<GameObject>();
    public List<DungeonData> DungeonDatas;

    
    

   
    public List<GameObject> Build()
    {     
        //10개 모두 빌드
        AllBuild();
        return dungeons;
    }
    public DungeonController Build(int level)
    {
        BuildToFlow(level);
        //던전은 레벨이 아닌 인덱스 기준
        return dungeons[level - 1].GetComponent<DungeonController>();
    }

    /// <summary>
    /// 룸,몬스터,아이템을 담을 빈오브젝트 생성
    /// </summary>
    /// <param name="model"></param>
    private DungeonController CreateHolder(DungeonData model)
    {
        if(monsterLvDatas == null)
        {
            monsterLvDatas = new Dictionary<int, List<MonsterLevelDataSO>>();            
            foreach(var data in monsterLevel)
            {
                if (monsterLvDatas.ContainsKey(data.level) == false)
                    monsterLvDatas.Add(data.level, new List<MonsterLevelDataSO>());

                monsterLvDatas[data.level].Add(data);
            }
        }


        #region Create Holders
        dungeonController  = Instantiate(dungeonControllerPrfab);
        dungeonController.name = "Dungeon " + model.name;

        roomHolder = new GameObject();
        roomHolder.name = "RoomHolder " + model.name;
        roomHolder.transform.SetParent(dungeonController.transform);

        monsterHolder = new GameObject();
        monsterHolder.name = "MonsterHolder " + model.name;
        monsterHolder.transform.SetParent(dungeonController.transform);

        itemHolder = new GameObject();
        itemHolder.name = "ItemHolder " + model.name;
        itemHolder.transform.SetParent(dungeonController.transform);
        dungeons.Add(dungeonController);
        #endregion

        return dungeonController.GetComponent<DungeonController>();
    }

    /// <summary>
    /// 던전을 인스턴스합니다.
    /// </summary>
    /// <param name="model"></param>
    public void RoomBuild(DungeonData model)
    {
        //룸 생성
        DungeonController dc = CreateHolder(model);
        dic.Clear();
        foreach (var roomData in model.rooms)
        {
            var Obj_Room = Instantiate(roomprefab);
            Room roomCompo = Obj_Room.GetComponent<Room>();
            dc.rooms.Add(roomCompo);
            Obj_Room.transform.SetParent(roomHolder.transform);

            roomCompo.Init(roomData);
            dic.Add(roomCompo.Guid, roomCompo);
           
        }
        
        //룸간의 포탈 셋업
        foreach (var linkData in model.links)
        {
            //시작점 
            var input = dic[linkData.fromNodeGUID];
            //상호작용된 문 탐색

            string outputDirection = linkData.direction.Split(' ')[1];
            outputDirection.Trim();
            eDirection direction = (eDirection)System.Enum.Parse(typeof(eDirection), outputDirection);
            
            var portal = input.keyValuePairs[direction];
            eDirection flip_direction = GetFlipDirection(direction);

            portal.toNodeGUID = linkData.toNodeGUID;
            portal.toNextRoom = dic[linkData.toNodeGUID];
            portal.toNextPoint = dic[linkData.toNodeGUID].keyValuePairs[flip_direction].PortalPoint;
            input.enablePortal.Add(portal);
            //portal.gameObject.SetActive(true);
            dc.portals.Add(portal);
        }        
    }

    private eDirection GetFlipDirection(eDirection direction)
    {
        switch(direction)
        {
            case eDirection.Left:
                return eDirection.Right;
            case eDirection.Right:
                return eDirection.Left;
            case eDirection.Top:
                return eDirection.Bottom;
            case eDirection.Bottom:
                return eDirection.Top;
        }
        return eDirection.Top;
    }


    /// <summary>
    /// 계층에 맞는 룸을 생상합니다.
    /// 계층에 맞는 아이템,몬스터를 생산합니다.
    /// </summary>
    /// <param name="curflow"></param>
    public void BuildToFlow(int curflow)
    {
        RoomBuild(DungeonDatas[curflow - 1]);
        CreateMonsterAndItem(curflow);
    }



    public void AllBuild()
    {
        int i = 0;        
        foreach (var roomData in DungeonDatas)
        {
            RoomBuild(roomData);
            CreateMonsterAndItem(i + 1);
            i++;
        }

        i = 0;
        foreach (var dungeon in dungeons)
        {
            if (i > 0)
                dungeon.gameObject.SetActive(false);
            i++;
        }
    }
    /// <summary>
    /// 던전에 아이템과 몬스터를 인스턴스합니다.
    /// </summary>
    /// <param name="level"></param>
    public void CreateMonsterAndItem(int level)
    {        
        DungeonRoomConfig config = dungeonConfig.GetConfigByLevel(level);
        DungeonController dc = dungeonController.GetComponent<DungeonController>();
        int itemCount = Random.Range(config.minItems, config.maxItems + 1);
        int monsterCount = Random.Range(config.minMonsters, config.maxMonsters + 1);
        foreach (var pair in dic)
        {
            Room room = pair.Value;

            switch (room.roomType)
            {
                case eRoomType.Item:

                    int tier = RollLevel(
                            config.itemTier1Chance,
                            config.itemTier2Chance,
                            config.itemTier3Chance);
                    GameObject newCreatedItem = room.grid.CreateItem(itemHolder.transform, tier);
                    dc.items.Add(newCreatedItem);
                    break;
                case eRoomType.Monster:

                    for(int i = 1; i <= monsterCount; i++)
                    {
                        int rolllevel = RollLevel(config.monsterLevel1Chance, config.monsterLevel2Chance, config.monsterLevel3Chance);
                        int rollData = Random.Range(1, 3);                        
                        MonsterLevelDataSO  modelData = monsterLvDatas[rolllevel][rollData];
                        Monster createdMonster = room.grid.CreateMonster(monsterHolder.transform);
                        createdMonster.name = i + createdMonster.name;
                        createdMonster.transform.localScale *= modelData.size;
                        createdMonster.SetData(modelData);

                        room.roomMonsters.Add(createdMonster);
                        dc.monsters.Add(createdMonster);
                        createdMonster.OnDeath += room.OnMonsterDeath;
                    }
                    break;
                case eRoomType.Start:
                    var rnd =  Random.Range(0, 2);
                    var node =  room.grid.GetRandomGridNode();
                    ResourceManager.Instance.CreateweaponItemToIndex(rnd, node.transform);
                    break;
            }

        }
    }




    int RollLevel(int chance1, int chance2, int chance3)
    {
        int roll = Random.Range(0, 100);
        if (roll < chance1) return 1;
        else if (roll < chance1 + chance2) return 2;
        else return 3;
    }   
}