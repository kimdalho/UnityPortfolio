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

    //포워드 방향 카운트
    [SerializeField] public int fireCount = 1; // 발사 횟수
    //멀티플 스킬 획득 시
    [SerializeField] public float fireMultypleAngleRange = 20f;
    [SerializeField] public int fireMultypleCount = 1;

    public eWeaponType eWeaponType;
    
    public eTagType Equip_state_Tag = eTagType.Equip_Weapon_State_default;
    
    //공격중일때
    protected eTagType stateTagType = eTagType.Attacking;
    protected readonly string String_AttackTrigger = "Trg_Attack";
    
    protected override IEnumerator ExecuteAbility()
    {         
        Character character = owner.GetComponent<Character>();
        Animator animator = character.GetAnimator();
        //character.GetAnimator().SetTrigger("Trg_Attack");
        //AnimatorStateInfo animationState = animator.GetCurrentAnimatorStateInfo(0);
        //float animationDuration = animationState.length;
        //if (animationDuration > Duration)
        //{
        //    Duration = animationDuration;
        //}

        GameAbilityTask animTask = new GameAbilityTask(owner);
        Task task = animTask.AnimExecute(AnimState.Attack);
        //yield return new WaitForTask(task);






        owner.GetGameplayTagSystem().AddTag(stateTagType);

        CreateDetectObject();
       
        float delay = Duration / Mathf.Max(character.attribute.GetCurValue(eAttributeType.AttackSpeed), 0.01f); // 0으로 나누는 것 방지
        Debug.Log($"Duration {Duration} , {delay}");
        yield return new WaitForSeconds(delay);  // 지속 효과 처리
        EndAbility();
    }


    private void CreateDetectObject()
    {
        float takeDamage = owner.attribute.GetCurValue(eAttributeType.Attack);
        Vector3 spherePosition = owner.transform.position + owner.transform.forward * 1f; // 정면에서 +3 이동
        int layerMask = LayerMask.GetMask("Item"); // "Enemy" 레이어만 감지
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

            Debug.Log("Detected: " + col.name);
        }
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
    