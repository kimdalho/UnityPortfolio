using System.Collections;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem.XInput;
using static UnityEngine.UI.GridLayoutGroup;

public class AttackAbility : GameAbility
{
    protected override IEnumerator ExecuteAbility()
    {
        Character character = owner.GetComponent<Character>();
        Animator animator = character.GetAnimator();

        if(animator == null)
        {
            Debug.LogWarning($"Not Found Animation {character.gameObject.name}");
            yield break;
        }

        character.GetAnimator().SetTrigger("Trg_Attack");

        while(animator.GetCurrentAnimatorStateInfo(0).IsTag("AttackTag"))
        {
            Debug.Log("Attack �±� ���°� ��� ���Դϴ�.");
            yield return null;
        }

        CreateDetectObject();
        //yield return new WaitForSeconds(Duration);  // ���� ȿ�� ó��
        EndAbility();
    }


    private void CreateDetectObject()
    {
        Vector3 spherePosition = owner.transform.position + owner.transform.forward * 1f; // ���鿡�� +3 �̵�
        int layerMask = LayerMask.GetMask("Item"); // "Enemy" ���̾ ����
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
    