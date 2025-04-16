using System.Collections;
using UnityEngine;
/*
Stunned, //스턴
ninjahead, ///// 피격 시 10% 확률로 1초간 무적
alienhead, /// 	피격 시 10% 확률로 1초간 무적
bearhead, /// 적 10명 처치 시 체력 1 회복
grasshead, // 공격 시 10% 확률로 피해 1 추가
Roostershead, // 투사체가 적중 시 독 피해 1 추가
clownhair, // 투사체 3개 발사
boxhead, // 3초마다 자동으로 투사체 1개 발사
ninjabody, // 적 처치 시 30초간 공속 30% 증가
alienbody, // 투사체가 벽에 튕겨 반사됨
beartorso, // 공격 시 10% 확률로 투사체 2개 좌우로 추가 발사
grasstrunk, // 공격력 20% 고정 증가
Roostersbody, //적 처치 시 30%확률로 최대 체력 추가 획득
clowntorso, // 	스킬 사용 시 다음 투사체가 관통 효과 추가
boxbody, // 이동 후 첫 공격에 투사체 2개 추가 발사
*/

public class GA_AlienBody : GameAbility 
{

    protected override IEnumerator ExecuteAbility()
    {
        yield return null;
    }
}