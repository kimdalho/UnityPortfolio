using UnityEngine;
using System.Collections.Generic;

public enum eRoomType
{
    None = 0,
    Monster, //���Ͱ� �����¹�
    Item,
    WeaponRoom,
    SacrificeRoom, //�����
    NPCRoom, //NPC��
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

