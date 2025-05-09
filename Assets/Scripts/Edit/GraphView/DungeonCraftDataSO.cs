using UnityEngine;
using System.Collections.Generic;

public enum eRoomType
{
    None = 0,
    Monster, //몬스터가 나오는방
    Item,
    WeaponRoom,
    SacrificeRoom, //희생방
    NPCRoom, //NPC룸
    Start,
    Boss,
    Empty
}

[CreateAssetMenu(menuName = "Dungeon/Graph Data")]
public class DungeonCraftDataSO : ScriptableObject
{
    public List<DungeonData> dungeonDatas = new List<DungeonData>();
    public List<RoomLinkData> links = new List<RoomLinkData>();
}

