using System.Collections;
using UnityEngine;

public interface IProjectileCountModifiable
{
    public void SetFireMultypleCount(eModifier type, int count);
}
/// <summary>
/// 라이플은 플레이어가 사용하는 무기 타입입니다.
/// 라이플은 발동시 3발의 총알(fireCount)이 0.3초 간격으로 기본적으로 발사합니다.
/// 플레이어가 획득한 스킬들에의해 다발사격(fireMultypleCount) 및 기본 총알(fireCount)의 카운트가 변형될 수 있습니다.
/// 변형이 가능한 스킬이란것을 알리기위해 IProjectileCountModifiable 인터페이스를 상속받습니다.
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
            // 손(Arm) 위치 탐색 (자식 → 자식으로 8단계 탐색)
            armTransform = owner.transform.GetChild(0).GetChild(owner.transform.GetChild(0).childCount - 1);
            for (int i = 0; i < 8; i++)
                armTransform = armTransform.GetChild(0);
        }
        
        // 탄환 생성
        yield return Shoot(armTransform);

        float delay = Duration / Mathf.Max(owner.attribute.GetCurValue(eAttributeType.AttackSpeed), 0.01f); // 어택 스피드가 0인경우는 없지만 혹시 모르니

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