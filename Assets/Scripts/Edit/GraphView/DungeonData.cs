using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Dungeon/Graph Data")]
public class DungeonData : ScriptableObject
{
    public List<RoomData> rooms = new List<RoomData>();
    public List<RoomLinkData> links = new List<RoomLinkData>();
}

[System.Serializable]
public class RoomData
{
    public string guid;
    public Vector2 position;
    public eRoomType roomType;
}

[System.Serializable]
public class RoomLinkData
{
    public string fromNodeGUID;
    public string toNodeGUID;
    public string direction; // ex: "North", "East", ...
}