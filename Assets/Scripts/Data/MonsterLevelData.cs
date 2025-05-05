using System.Drawing;
using UnityEngine;


[CreateAssetMenu(fileName = "MonsterLevelTable", menuName = "GameData/Monster Level Table")]

public class MonsterLevelDataSO : ScriptableObject
{
    public int level;
    public GameAttribute attribute;
    public float attackRange; //만들긴했는데 엄두가 안나네
    public float size = 1f;

}