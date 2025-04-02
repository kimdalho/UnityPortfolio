using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 드롭 아이템에 사용될 변수들 묶음
/// 플레이어가 너무 무거워진다 변수라도 좀 나눠 놓자
/// </summary>
public partial class Player : Character
{
    public float magnetRange = 3f;  // 흡수 범위
    public float magnetPower = 1.5f;  // 흡수 속도 가중치
    //해쉬셋으로 만든 이유
    //리스트야 당연히 중복되는
    //요소값이 들어갈 수 있으니 제외하고
    //사실 이런 구조를 만들때 항상 딕셔너리를 사용했었지만
    //굳이 키벨류페어 형태로 메모리를 낭비할 필요가 없다고 느낌
    private HashSet<DroppedItem> nearbyItems = new HashSet<DroppedItem>();

    void DropItemUpdate()
    {
        Vector3 playerPos = transform.position;
        float magnetRangeSqr = magnetRange * magnetRange; // 거리 비교 최적화

        foreach (var item in nearbyItems)
        {
            float distanceSqr = (item.transform.position - playerPos).sqrMagnitude;
            if (distanceSqr <= magnetRangeSqr) // 일정 범위 내일 때만
            {
                item.AttractToPlayer(playerPos, magnetPower);
            }
        }
    }
}
