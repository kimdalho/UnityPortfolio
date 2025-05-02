using System.Collections;
using UnityEngine;

public class GA_Attack_Rifle : AttackAbility
{
    [SerializeField] private int fireCount = 3; // �߻� Ƚ��
    [SerializeField] private float fireAngleRange = 20f;
    [SerializeField] private ProjectileType projectileType;
    [SerializeField] private float delayAtkTime = 0.3f;

    private void Awake()
    {
        Duration = 4;
    }

    protected override IEnumerator ExecuteAbility()
    {
        bool condition = owner.gameplayTagSystem.HasTag(eTagType.Attacking);
        if (condition)
            yield break;

        owner.gameplayTagSystem.AddTag(eTagType.Attacking);
       

        float angleRange = (fireCount == 1) ? 0f : fireAngleRange;
        Vector3 forward = owner.transform.forward;
        Vector3 startDir = Quaternion.AngleAxis(-angleRange, Vector3.up) * forward;
        Vector3 endDir = Quaternion.AngleAxis(angleRange, Vector3.up) * forward;

        // ��(Arm) ��ġ Ž�� (�ڽ� �� �ڽ����� 8�ܰ� Ž��)
        Transform armTransform = owner.transform.GetChild(0).GetChild(owner.transform.GetChild(0).childCount - 1);
        for (int i = 0; i < 8; i++)
            armTransform = armTransform.GetChild(0);



        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        Vector3 targetPoint;

        if (Physics.Raycast(ray, out var hit))
            targetPoint = hit.point;
        else
            targetPoint = ray.origin + ray.direction * 100f;

        Vector3 direction = (targetPoint - armTransform.position).normalized;

        // źȯ ����
        for (int i = 0; i < fireCount; i++)
        {
            owner.GetAnimator().SetTrigger("Trg_Attack");
            float t = (i + 1) / (float)fireCount;
            //�ݵ� �ϴ� ��Ȱ��ȭ
            //Vector3 direction = Vector3.Lerp(startDir, endDir, t).normalized;

            var projectile = ProjectileFactory.Instance.GetProjectile(projectileType, armTransform.position, Quaternion.identity);
            projectile.Initialized(owner, direction, targetMask, AbilityTag, true);
            // �߻� �� ������
            yield return new WaitForSeconds(delayAtkTime);
        }

   


        // �ִϸ��̼� ���� �ð� ��� ������
        //float animLength = owner.GetAnimator().GetCurrentAnimatorStateInfo(0).length;
        //float duration = Mathf.Max(Duration);
        float delay = Duration / Mathf.Max(owner.attribute.attackSpeed, 0.01f); // ���� ���ǵ尡 0�ΰ��� ������ Ȥ�� �𸣴�

        yield return new WaitForSeconds(delay);

        EndAbility();
    }

}