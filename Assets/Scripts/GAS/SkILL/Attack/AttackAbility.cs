using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// 1.플레이어 몬스터 모두 Attack어빌리티를 가지고있다
/// 2.플레이어 몬스터 모두 tagAttack을 가지고있다.
/// 3.플레이어의 경우 다양한 무기를 장착할수있다.
/// 4.플레이어가 무기를 교체할경우 기존에 tag된 Ability를 삭제한다.
/// 5.플레이어는 Equip_state_Tag로 현제 장착된 무기타입을 gameplayTagSystem에 알려준다.
/// 
/// </summary>
public class AttackAbility : GameAbility
{
    [Header("무기 셋팅")]
    protected WeaponSetting weaponSetting;

    //포워드 방향 카운트
    [SerializeField] public int fireCount = 1; // 발사 횟수
    //멀티플 스킬 획득 시
    [SerializeField] public float fireMultypleAngleRange = 20f;
    [SerializeField] public int fireMultypleCount = 1;

    public eWeaponType eWeaponType;
    
    public eTagType Equip_state_Tag = eTagType.Equip_Weapon_State_default;

    private bool isExecuting = false;

    //공격중일때
    protected eTagType stateTagType = eTagType.Attacking;

    protected override IEnumerator ExecuteAbility()
    {
        Task task = animTask.AnimExecute(AnimState.Attack);        
        yield return new WaitForTask(task);
        owner.GetGameplayTagSystem().AddTag(stateTagType);
            
        EndAbility();
    }


    private void CreateDetectObject()
    {
        float takeDamage = owner.attribute.GetCurValue(eAttributeType.Attack);
        Vector3 spherePosition = owner.transform.position + owner.transform.forward * 1f; // 정면에서 +3 이동
        Collider[] results = SphereDetector.DetectObjectsInSphere(spherePosition, 1, targetMask);
        foreach (var col in results)
        {
            AttributeEntity ae = col.GetComponent<AttributeEntity>();
            if (ae != null) 
            {

                var character = ae as Character;

                if (character != null && character.fxSystem != null)
                {
                    GameEffect effect = new GameEffect(eModifier.Add);
                    effect.AddModifier(eAttributeType.Health, -takeDamage);
                    ae.ApplyEffect(effect);
                }
                else
                {
                    Debug.LogWarning("맞은 대상 FX가 없다");
                }
            }
        }
    }

    protected override void OnFireAnimationApply()
    {
        base.OnFireAnimationApply();
        CreateDetectObject();
    }

    public override void EndAbility()
    {
        base.EndAbility();
        if (owner == null)
            return;
        
        if(owner.GetGameplayTagSystem().HasTag(stateTagType))       
            owner.GetGameplayTagSystem().RemoveTag(stateTagType);
    }

}
    