using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DungeonLevelTable", menuName = "GameData/Dungeon Config Table")]
public class DungeonConfigSO : ScriptableObject
{
    public List<DungeonRoomConfig> roomConfigs;

    public DungeonRoomConfig GetConfigByLevel(int level)
    {
        return roomConfigs.Find(config => config.dungeonLevel == level);
    }
}