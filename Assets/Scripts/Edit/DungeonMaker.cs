using System.Collections.Generic;
using UnityEngine;

public class DungeonMaker : MonoBehaviour
{
    public DungeonData roomData;
    public GameObject roomprefab;
    public int offsetX;
    public int offsetY;

    public Dictionary<string, Room> dic;

    //던전 데이터
    public DungeonConfigSO dungeonConfig;
    public int currentDungeonLevel = 5;

    //홀더
    private GameObject dg;
    private GameObject itemHolder;
    private GameObject roomHolder;
    private GameObject monsterHolder;
    List<GameObject> dungeons;
    public List<DungeonData> DungeonDatas;
    

    public List<GameObject> Build()
    {
        dic = new Dictionary<string, Room>();
            //10개 모두 빌드
            AllBuild();
        return dungeons;
    }
    public List<GameObject> Build(int index)
    {                 
         AllBuild();     
        return dungeons;
    }


    private void CreateHolder(DungeonData model)
    {
        #region Create Holders
        dg = new GameObject();
        dg.name = "Dungeon " + model.name;

        roomHolder = new GameObject();
        roomHolder.name = "RoomHolder " + model.name;
        roomHolder.transform.SetParent(dg.transform);

        monsterHolder = new GameObject();
        monsterHolder.name = "MonsterHolder " + model.name;
        monsterHolder.transform.SetParent(dg.transform);

        itemHolder = new GameObject();
        itemHolder.name = "ItemHolder " + model.name;
        itemHolder.transform.SetParent(dg.transform);
        dungeons.Add(dg);
        #endregion
    }


    public void RoomBuild(DungeonData model)
    {
        
        CreateHolder(model);
        foreach (var roomData in model.rooms)
        {
            var Obj_Room = Instantiate(roomprefab);
            Room roomCompo = Obj_Room.GetComponent<Room>();
            Obj_Room.transform.SetParent(roomHolder.transform);

            roomCompo.Init(roomData);
            dic.Add(roomCompo.Guid, roomCompo);
           
        }
        

        foreach (var linkData in model.links)
        {
            //시작점 
            var input = dic[linkData.fromNodeGUID];
            //상호작용된 문 탐색

            string outputDirection = linkData.direction.Split(' ')[1];
            outputDirection.Trim();
            eDirection direction = (eDirection)System.Enum.Parse(typeof(eDirection), outputDirection);

            var portal = input.keyValuePairs[direction];
            portal.toNodeGUID = linkData.toNodeGUID;
            portal.toNodePos = dic[linkData.toNodeGUID].transform.position;
            portal.gameObject.SetActive(true);
        }

        
    }



    public void AllBuild()
    {
        int i = 0;
        dungeons = new List<GameObject>();
        foreach (var roomData in DungeonDatas)
        {
            RoomBuild(roomData);
            SetData(i + 1);
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

    public void SetData(int level)
    {        
        DungeonRoomConfig config = dungeonConfig.GetConfigByLevel(level);
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
                    room.grid.CreateItem(itemHolder.transform, tier);

                    break;
                case eRoomType.Monster:

                    for(int i = 1; i <= monsterCount; i++)
                    {
                        int rolllevel = RollLevel(config.monsterLevel1Chance, config.monsterLevel2Chance, config.monsterLevel3Chance);
                        room.grid.CreateMonster(monsterHolder.transform, rolllevel);
                        Debug.Log($"Spawn Monster Lv{rolllevel}");
                    }
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