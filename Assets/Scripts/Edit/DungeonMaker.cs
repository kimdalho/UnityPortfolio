#if UNITY_EDITOR

using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DungeonMaker : MonoBehaviour
{
    public DungeonData roomData;
    public GameObject roomprefab;

    public int offsetX;
    public int offsetY;

    public Dictionary<string, Room> dic;


   
    public void Create()
    {
        var temp = new GameObject();
        temp.name = "Dungeon";
        

        dic = new Dictionary<string, Room>();
        foreach (var roomData in roomData.rooms) 
        {
            var Obj_Room  = Instantiate(roomprefab);
            Room roomCopo = Obj_Room.GetComponent<Room>();
            Obj_Room.transform.SetParent(temp.transform);

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





}
#endif