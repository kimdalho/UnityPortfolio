using System.Drawing;
using UnityEngine;


[CreateAssetMenu(fileName = "MonsterLevelTable", menuName = "GameData/Monster Level Table")]

public class MonsterLevelDataSO : ScriptableObject
{
    public int level;
    public GameAttribute attribute;
    public float attackRange; //������ߴµ� ���ΰ� �ȳ���
    public float size = 1f;

}