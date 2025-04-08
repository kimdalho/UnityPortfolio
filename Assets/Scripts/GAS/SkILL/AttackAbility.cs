using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem.XInput;
using static UnityEngine.UI.GridLayoutGroup;

public class AttackAbility : GameAbility
{
    protected override IEnumerator ExecuteAbility()
    {
        Animator animator = owner.GetComponent<Animator>();
        animator.SetTrigger("Trg_Attack");

        AnimatorStateInfo animationState = animator.GetCurrentAnimatorStateInfo(0);
        float animationDuration = animationState.length;
        if(animationDuration > Duration)
        {
            Duration = animationDuration;
        }
        CreateDetectObject();
        yield return new WaitForSeconds(Duration);  // 지속 효과 처리
        EndAbility();
    }


    private void CreateDetectObject()
    {
        Vector3 spherePosition = owner.transform.position + owner.transform.forward * 1f; // 정면에서 +3 이동
        int layerMask = LayerMask.GetMask("Item"); // "Enemy" 레이어만 감지
        Collider[] results = SphereDetector.DetectObjectsInSphere(spherePosition, 1, LayerMask.GetMask("Item"));
        foreach (var col in results)
        {
            //col.GetComponent<PlaceableObject>().TakeDamage(1);
            AttributeEntity ae = col.GetComponent<AttributeEntity>();
            if (ae != null) 
            {      
                var effect = new GameEffect(new DamageExecution());
                effect.Apply(owner, ae);
            }

            Debug.Log("Detected: " + col.name);
        }
    }



}
    