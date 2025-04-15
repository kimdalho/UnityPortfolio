using UnityEngine;

[System.Serializable]
public class DungeonRoomConfig
{

    [Header("Dungeon Level")]
    public int dungeonLevel;

    [Header("Monster Count Range")]
    public int minMonsters;
    public int maxMonsters;

    [Header("Monster Level Probabilities (0~100)")]
    [Range(0, 100)] public int monsterLevel1Chance;
    [Range(0, 100)] public int monsterLevel2Chance;
    [Range(0, 100)] public int monsterLevel3Chance;

    [Header("Item Count Range")]
    public int minItems;
    public int maxItems;

    [Header("Item Tier Probabilities (0~100)")]
    [Range(0, 100)] public int itemTier1Chance;
    [Range(0, 100)] public int itemTier2Chance;
    [Range(0, 100)] public int itemTier3Chance;
}
