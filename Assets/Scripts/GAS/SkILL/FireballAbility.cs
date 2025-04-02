
using System.Collections;
using UnityEngine;

public class FireballAbility : GameAbility
{
    protected override IEnumerator ExecuteAbility()
    {
        Debug.Log(" 파이어볼 발사!");        
        yield return new WaitForSeconds(Duration);  // 지속 효과 처리
        Debug.Log(" 파이어볼 종료!");
        EndAbility();
    }
}
