#if UNITY_EDITOR

using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.TextCore.Text;
using static Cinemachine.DocumentationSortingAttribute;
using static UnityEngine.Rendering.STP;

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
    private GameObject room;
    private GameObject monsterHolder;





    public void Create()
    {
        CreateHolder();
        Build();
        SetData();
    }

    private void CreateHolder()
    {
        #region Create Holders
        dg = new GameObject();
        dg.name = "Dungeon " + roomData.name;

        room = new GameObject();
        room.name = "RoomHolder " + roomData.name;
        room.transform.SetParent(dg.transform);

        monsterHolder = new GameObject();
        monsterHolder.name = "MonsterHolder " + roomData.name;
        monsterHolder.transform.SetParent(dg.transform);

        itemHolder = new GameObject();
        itemHolder.name = "ItemHolder " + roomData.name;
        itemHolder.transform.SetParent(dg.transform);
        #endregion
    }


    public void Build()
    {
        dic = new Dictionary<string, Room>();
        foreach (var roomData in roomData.rooms)
        {
            var Obj_Room = Instantiate(roomprefab);
            Room roomCopo = Obj_Room.GetComponent<Room>();
            Obj_Room.transform.SetParent(dg.transform);

            roomCopo.Init(roomData);
            dic.Add(roomCopo.Guid, roomCopo);
        }

        foreach (var linkData in roomData.links)
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





    public void SetData()
    {
        DungeonRoomConfig config = dungeonConfig.GetConfigByLevel(currentDungeonLevel);
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
                        int level = RollLevel(config.monsterLevel1Chance, config.monsterLevel2Chance, config.monsterLevel3Chance);
                        room.grid.CreateMonster(monsterHolder.transform, level);
                        Debug.Log($"Spawn Monster Lv{level}");
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

    
    void SpawnItem(int tier) => Debug.Log($"Spawn Item Tier{tier}");

    


}
#endif