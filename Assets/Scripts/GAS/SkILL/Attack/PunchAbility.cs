using System.Collections;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.InputSystem.XInput;
using static UnityEngine.UI.GridLayoutGroup;

public class PunchAbility : AttackAbility
{
    protected override IEnumerator ExecuteAbility()
    {         
        Character character = owner.GetComponent<Character>();
        Animator animator = character.GetAnimator();
        character.GetAnimator().SetTrigger("Trg_Attack");
        owner.gameplayTagSystem.AddTag(eTagType.Attack);


        AnimatorStateInfo animationState = animator.GetCurrentAnimatorStateInfo(0);
        float animationDuration = animationState.length;
        if(animationDuration > Duration)
        {
            Duration = animationDuration;
        }
        CreateDetectObject();
       
        float delay = Duration / Mathf.Max(character.attribute.attackSpeed, 0.01f); // 0���� ������ �� ����
        Debug.Log($"Duration {Duration} , {delay}");
        yield return new WaitForSeconds(delay);  // ���� ȿ�� ó��
        EndAbility();
    }


    private void CreateDetectObject()
    {
        Vector3 spherePosition = owner.transform.position + owner.transform.forward * 1f; // ���鿡�� +3 �̵�
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
                    Debug.LogWarning("���� ��� FX�� ����");
                }
            }

            Debug.Log("Detected: " + col.name);
        }
    }
    
    public override void EndAbility()
    {       
        if(owner.gameplayTagSystem.HasTag(eTagType.Attack))       
            owner.gameplayTagSystem.RemoveTag(eTagType.Attack);
    }



}
    