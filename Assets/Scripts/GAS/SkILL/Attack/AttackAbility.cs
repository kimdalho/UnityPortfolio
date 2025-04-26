using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Android;

public class AttackAbility : GameAbility
{
    public eWeaponType eWeaponType;

    protected eTagType stateTagType = eTagType.Attacking;
    
    protected override IEnumerator ExecuteAbility()
    {         
        Character character = owner.GetComponent<Character>();
        Animator animator = character.GetAnimator();
        character.GetAnimator().SetTrigger("Trg_Attack");
        owner.gameplayTagSystem.AddTag(stateTagType);


        AnimatorStateInfo animationState = animator.GetCurrentAnimatorStateInfo(0);
        float animationDuration = animationState.length;
        if(animationDuration > Duration)
        {
            Duration = animationDuration;
        }
        CreateDetectObject();
       
        float delay = Duration / Mathf.Max(character.attribute.attackSpeed, 0.01f); // 0으로 나누는 것 방지
        Debug.Log($"Duration {Duration} , {delay}");
        yield return new WaitForSeconds(delay);  // 지속 효과 처리
        EndAbility();
    }


    private void CreateDetectObject()
    {
        Vector3 spherePosition = owner.transform.position + owner.transform.forward * 1f; // 정면에서 +3 이동
        int layerMask = LayerMask.GetMask("Item"); // "Enemy" 레이어만 감지
        Collider[] results = SphereDetector.DetectObjectsInSphere(spherePosition, 1, targetMask);
        foreach (var col in results)
        {
            AttributeEntity ae = col.GetComponent<AttributeEntity>();
            if (ae != null) 
            {
                var effect = new DamageExecution();                
                effect.Execute(owner, ae);

                var character = ae as Character;
                if (character != null && character.fxSystem != null)
                {
                    //character.fxSystem?.ExecuteFX(AbilityTag);
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
        if(owner.gameplayTagSystem.HasTag(stateTagType))       
            owner.gameplayTagSystem.RemoveTag(stateTagType);
    }



}
    