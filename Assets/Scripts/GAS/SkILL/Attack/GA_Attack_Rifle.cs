using System.Collections;
using UnityEngine;

public interface IProjectileCountModifiable
{
    public void SetFireMultypleCount(eModifier type, int count);
}
/// <summary>
/// �������� �÷��̾ ����ϴ� ���� Ÿ���Դϴ�.
/// �������� �ߵ��� 3���� �Ѿ�(fireCount)�� 0.3�� �������� �⺻������ �߻��մϴ�.
/// �÷��̾ ȹ���� ��ų�鿡���� �ٹ߻��(fireMultypleCount) �� �⺻ �Ѿ�(fireCount)�� ī��Ʈ�� ������ �� �ֽ��ϴ�.
/// ������ ������ ��ų�̶����� �˸������� IProjectileCountModifiable �������̽��� ��ӹ޽��ϴ�.
/// </summary>
public class GA_Attack_Rifle : AttackAbility , IProjectileCountModifiable
{
    [SerializeField] private ProjectileType projectileType;
    private Transform armTransform;

    readonly WaitForSeconds delayAtkTime = new WaitForSeconds(0.3f);

    protected override IEnumerator ExecuteAbility()
    {
        bool condition = owner.GetGameplayTagSystem().HasTag(eTagType.Attacking);
        if (condition)
            yield break;

        owner.GetGameplayTagSystem().AddTag(eTagType.Attacking); 
        
        if(armTransform == null)
        {
            // ��(Arm) ��ġ Ž�� (�ڽ� �� �ڽ����� 8�ܰ� Ž��)
            armTransform = owner.transform.GetChild(0).GetChild(owner.transform.GetChild(0).childCount - 1);
            for (int i = 0; i < 8; i++)
                armTransform = armTransform.GetChild(0);
        }
        
        // źȯ ����
        yield return Shoot(armTransform);

        float delay = Duration / Mathf.Max(owner.attribute.GetCurValue(eAttributeType.AttackSpeed), 0.01f); // ���� ���ǵ尡 0�ΰ��� ������ Ȥ�� �𸣴�

        yield return new WaitForSeconds(delay);

        EndAbility();
    }

    protected virtual IEnumerator CoMultypleShoot(Transform armTransform)
    {
        float angleRange = (fireMultypleCount == 1) ? 0f : fireMultypleAngleRange;

        Vector3 forward = owner.transform.forward;
        Vector3 startDir = Quaternion.AngleAxis(-angleRange, Vector3.up) * forward;
        Vector3 endDir = Quaternion.AngleAxis(angleRange, Vector3.up) * forward;

        for (int i = 0; i < fireMultypleCount; i++)
        {
            float t = (fireMultypleCount == 1) ? 0.5f : i / (float)(fireMultypleCount - 1);
            Vector3 direction = Vector3.Lerp(startDir, endDir, t).normalized;
            var projectile = ProjectileFactory.Instance.GetProjectile(projectileType, armTransform.position, Quaternion.identity);
            projectile.Initialized(owner, direction, targetMask, AbilityTag, true);                     
        }
        SoundManager.instance.PlayEffect(eEffectType.Shoot, armTransform);
        yield return delayAtkTime;
    }

    protected virtual IEnumerator Shoot(Transform armTransform)
    {
        for (int i = 0; i < fireCount; i++)
        {
            owner.GetAnimator().SetTrigger(String_AttackTrigger);
            yield return CoMultypleShoot(armTransform);
        }
    }

    public void SetFireMultypleCount(eModifier type, int count)
    {
        switch(type)
        {
            case eModifier.Add:
                fireMultypleCount += count;
                break;
            default:
                Debug.LogError("GA_Attack_Rifle -> Non-existent modifier type");
                break;
        }
    }
}