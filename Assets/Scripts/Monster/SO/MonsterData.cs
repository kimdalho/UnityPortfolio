using UnityEngine;

[CreateAssetMenu(fileName = "MonsterData", menuName = "Monster/MonsterData")]
public class MonsterData : ScriptableObject
{
    public int rank;    // 몬스터 티어
    public int id;      // 프리팹 ID
    
}
