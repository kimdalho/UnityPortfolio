using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoomFactory : MonoBehaviour, IFactory<RoomPrefabSO, Room>
{
    public List<RoomPrefabSO> RoomList;

    public Room CreateRoomToType(eRoomType type, Transform parent)
    {
        var data = RoomList.FirstOrDefault(x => x.roomType == type);
        if (data == null)
        {
            Debug.LogError($"Room of type {type} not found");
            return null;
        }
        return Create(data, parent);
    }


    public Room Create(RoomPrefabSO data, Transform parent)
    {
        GameObject go = Instantiate(data.prefab, parent);
        Room room = go.GetComponent<Room>();
        room.SetData(data);
        return room;
    }
}
