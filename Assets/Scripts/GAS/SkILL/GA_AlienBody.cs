using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Member;
/*
Stunned, //스턴
ninjahead, /        피격 시 15% 확률로 1초간 무적
alienhead, /        적 처치 시 20% 확률로 최대체력 1증가
bearhead, //        적 4명 처치 시 체력 1 회복
grasshead, /        공격 시 10% 확률로 피해 1 추가
Roostershead        투사체가 적중 시 독 피해 1 추가
clownhair, /        투사체 3개 발사
boxhead, //         3초마다 자동으로 투사체 1개 발사
ninjabody, /        적 처치 시 30초간 공속 30% 증가
alienbody, /        25초 마다 90% 확률로 위성 생성
beartorso, /        공격 시 60% 확률로 투사체 2개 좌우로 추가 발사
grasstrunk,         현제 공격력 1.5배 증가
Roostersbody        적 처치 시 30%확률로 최대 체력 추가 획득
clowntorso,         스킬 사용 시 다음 투사체가 관통 효과 추가
boxbody, //         이동 후 첫 공격에 투사체 2개 추가 발사
*/

public class GA_AlienBody : GameAbility 
{
    //성공 확률 0~1; 1이면 100퍼센트 성공
    public float condtionper;
    /// 발동 딜레이 30이면 30초마다 트라이
    public float conditionSec;
    [Header("플라이 벨류")]
    public float radius = 2f;      // 중심에서의 거리
    public float speed = 50f;

    public int maxCount = 2; //생성 가능한 제한 수

    public List<Fly> flys;

    float deltime;

    


    private void Start()
    {
        flys = new List<Fly>();
    }


    protected override IEnumerator ExecuteAbility()
    {
        owner.gameplayTagSystem.AddTag(AbilityTag);
        while (true)
        {
            if (flys.Count >= maxCount)
                yield return new WaitForSeconds(3f);

            deltime += Time.deltaTime;
            if (deltime > conditionSec)
            {
                Debug.Log("타임 조건성공");
                float chance = Random.value; // 0.0 ~ 1.0
                Debug.Log($"찬스 {chance}");
                if (chance < condtionper) //0.3 이면 30퍼센트 성공
                {
                    deltime = 0;
                    Fly fly =  ResourceManager.Instance.CreateFly();
                    fly.gameObject.transform.SetParent(owner.transform);
                    fly.SetData(this,owner.transform);

                }
            }            
            yield return null;
        }        
    }

    public void RemoveFly(Fly fly)
    {
        flys.Remove(fly);
    }

    
}