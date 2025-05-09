using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonController : MonoBehaviour
{
    public List<Room> rooms = new List<Room>();
    public List<Portal> portals = new List<Portal>();
    public List<Monster> monsters = new List<Monster>();
    public List<EquipmentItem> items = new List<EquipmentItem>();

    public void EnterTheRoom(Room room)
    {
        switch (room.roomType)
        {
            case eRoomType.Item:
            case eRoomType.Start:
            case eRoomType.NPCRoom:
            case eRoomType.SacrificeRoom:
            case eRoomType.Empty:
                Debug.Log($"EnterTheRoom => {room.roomType} is clear");
                room.isClear = true;
                break;
            case eRoomType.Boss:
            case eRoomType.Monster:
            
                return;
            default:
                Debug.LogError(room.roomType + " not found");
                break;
        }
    }

    public void OnGameover()
    {
        foreach (Room room in rooms)
        {
            foreach (var monster in room.roomMonsters)
            {
                monster.onlyIdle = true;
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
