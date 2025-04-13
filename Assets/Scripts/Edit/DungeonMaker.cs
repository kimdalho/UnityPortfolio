#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;

public class DungeonMaker : MonoBehaviour
{
    public DungeonData roomData;
    public GameObject roomprefab;

    public int offsetX;
    public int offsetY;


   
    public void Create()
    {
        foreach (var room in roomData.rooms) 
        {
            var Obj_Room  = Instantiate(roomprefab);
            if(room.roomType == eRoomType.Start)
            {
                Obj_Room.transform.position = Vector3.zero;
            }
            else {
                Obj_Room.gameObject.name = room.roomType.ToString();
                Obj_Room.transform.position = new Vector3(room.position.x * offsetX, 0, room.position.y * offsetY);
            }

        }        
    }

}
#endif