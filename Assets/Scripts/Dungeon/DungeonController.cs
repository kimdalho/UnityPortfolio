using System.Collections.Generic;
using UnityEngine;

public class DungeonController : MonoBehaviour
{
    public List<Room> rooms = new List<Room>();
    public List<Portal> portals = new List<Portal>();
    public List<Monster> monsters = new List<Monster>();
    public List<GameObject> items = new List<GameObject>();

    public void Setup()
    {
        foreach (Room room in rooms)
        {
            switch(room.roomType)
            {
                case eRoomType.Item:
                case eRoomType.Start:
                case eRoomType.Boss:
                    room.isClear = true;
                    break;
            }
            
        }
    }

    public Room FindRoombyType(eRoomType type)
    {
        foreach (Room room in rooms)
        {
            if(room.roomType == type)
                return room;
        }
        Debug.LogError("해당 룸 타입은 존재하지않습니다.");
        return null;
    }
}
