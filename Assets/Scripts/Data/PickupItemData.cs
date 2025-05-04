using UnityEngine;

public enum eTagType
{   
    Stunned, //스턴
    ninjahead, ///// 
    alienhead, ///
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

    Attack,
    Attacking, //공격중
    FanShapeFire, // 투사체 발사 (Ninja, Soldier 사용)
    FlameThrower,  // 일정 시간동안 화염 방사 (Burner 사용)
    NinjaHead_State_Invincible, //

    NinjaBody_State_SpeedUp,// 닌자바디 스테이트

    Equip_Weapon_State_default, //플레이어가 장착한 아이템 타입 체크
    Equip_Weapon_State_Rifl, //플레이어가 장착한 아이템 타입 체크
    Equip_Weapon_State_Bazooka, //플레이어가 장착한 아이템 타입 체크
    Equip_Weapon_State_Handgun, //플레이어가 장착한 아이템 타입 체크

    Player_State_IgnoreInput, // 브릿지 타고있는중 이때는 입력받지않음
    Player_State_IgnorePortal, // 브릿지 타고있는중 이때는 입력받지않음
    Player_State_HasAttackTarget, // 공격 대상이 존재한다.

    Effect_NinjaSkill, //닌자 스킬 발동 시

}


/// <summary>
/// 혼선이 생기기 쉬운 이름으로 정해서 아쉽다
/// 머리와 바디 아이템만을 취급한다.
/// </summary>
[CreateAssetMenu(fileName = "NewItemData", menuName = "GameData/Item Data")]
public class PickupItemData : PickupItemDataBase
{
    //플레이어 인덱스
    public int modelIndex;

    public GameAttribute attribute;

    //오브젝트 인덱스
    public int objectIndex;
    //아이템 등급 높은 템일수록 상위 층에서만 드랍되는게 조건    
    public int Rank;
}

