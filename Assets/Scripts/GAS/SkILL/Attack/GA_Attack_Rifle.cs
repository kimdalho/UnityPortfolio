using System.Collections;
using UnityEngine;

public interface IProjectileCountModifiable
{
    public void SetFireMultypleCount(eModifier type, int count);
}

public class GA_Attack_Rifle : AttackAbility , IProjectileCountModifiable
{

    [SerializeField] private ProjectileType projectileType;
    [SerializeField] private float delayAtkTime = 0.3f;


    protected override IEnumerator ExecuteAbility()
    {
        bool condition = owner.gameplayTagSystem.HasTag(eTagType.Attacking);
        if (condition)
            yield break;

        owner.gameplayTagSystem.AddTag(eTagType.Attacking);        
        // ��(Arm) ��ġ Ž�� (�ڽ� �� �ڽ����� 8�ܰ� Ž��)
        Transform armTransform = owner.transform.GetChild(0).GetChild(owner.transform.GetChild(0).childCount - 1);
        for (int i = 0; i < 8; i++)
            armTransform = armTransform.GetChild(0);

        // źȯ ����
        yield return Shoot(armTransform);

        float delay = Duration / Mathf.Max(owner.attribute.attackSpeed, 0.01f); // ���� ���ǵ尡 0�ΰ��� ������ Ȥ�� �𸣴�

        yield return new WaitForSeconds(delay);

        EndAbility();
    }

    protected virtual IEnumerator CoMultypleShoot(Transform armTransform)
    {
        yield return null;
        float angleRange = (fireMultypleCount == 1) ? 0f : fireMultypleAngleRange;

        Vector3 forward = owner.transform.forward;
        Vector3 startDir = Quaternion.AngleAxis(-angleRange, Vector3.up) * forward;
        Vector3 endDir = Quaternion.AngleAxis(angleRange, Vector3.up) * forward;

        for (int i = 0; i < fireMultypleCount; i++)
        {
            float t = (i + 1) / (float)fireMultypleCount;
            Vector3 direction = Vector3.Lerp(startDir, endDir, t).normalized;
            var projectile = ProjectileFactory.Instance.GetProjectile(projectileType, armTransform.position, Quaternion.identity);
            projectile.Initialized(owner, direction, targetMask, AbilityTag, true);                     
        }
        SoundManager.instance.PlayEffect(eEffectType.Shoot, armTransform);
        yield return new WaitForSeconds(delayAtkTime);
    }

    protected virtual IEnumerator Shoot(Transform armTransform)
    {
        owner.GetAnimator().SetTrigger("Trg_Attack");

        for (int i = 0; i < fireCount; i++)
        {
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
        }
    }
}