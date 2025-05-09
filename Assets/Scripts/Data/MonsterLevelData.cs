using UnityEngine;


[CreateAssetMenu(fileName = "MonsterLevelTable", menuName = "GameData/Monster Level Table")]

public class MonsterDataSO : ScriptableObject
{
    public int level;
    public AttributeSet attribute;
    public float attackRange;
    public GameObject prefab;
}