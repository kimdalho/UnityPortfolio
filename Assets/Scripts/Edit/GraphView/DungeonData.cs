using UnityEngine;

[System.Serializable]
public class DungeonData
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