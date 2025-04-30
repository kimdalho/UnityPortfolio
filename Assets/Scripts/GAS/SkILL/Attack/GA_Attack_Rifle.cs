using System.Collections;
using UnityEngine;

public class GA_Attack_Rifle : AttackAbility
{
    [SerializeField] private int fireCount = 1; // 발사 횟수
    [SerializeField] private float fireAngleRange = 20f;
    [SerializeField] private ProjectileType projectileType;
    [SerializeField] private float delayAtkTime = 0.3f;

    private void Awake()
    {
        Duration = 5;
    }

    protected override IEnumerator ExecuteAbility()
    {
        bool condition = owner.gameplayTagSystem.HasTag(eTagType.Attacking);
        if (condition)
            yield break;

        owner.gameplayTagSystem.AddTag(eTagType.Attacking);
        owner.GetAnimator().SetTrigger("Trg_Attack");

        float angleRange = (fireCount == 1) ? 0f : fireAngleRange;
        Vector3 forward = owner.transform.forward;
        Vector3 startDir = Quaternion.AngleAxis(-angleRange, Vector3.up) * forward;
        Vector3 endDir = Quaternion.AngleAxis(angleRange, Vector3.up) * forward;

        // 손(Arm) 위치 탐색 (자식 → 자식으로 8단계 탐색)
        Transform armTransform = owner.transform.GetChild(0).GetChild(owner.transform.GetChild(0).childCount - 1);
        for (int i = 0; i < 8; i++)
            armTransform = armTransform.GetChild(0);

        // 발사 전 딜레이
        yield return new WaitForSeconds(delayAtkTime);

        // 탄환 생성
        for (int i = 0; i < fireCount; i++)
        {
            float t = (i + 1) / (float)fireCount;
            Vector3 direction = Vector3.Lerp(startDir, endDir, t).normalized;

            var projectile = ProjectileFactory.Instance.GetProjectile(projectileType, armTransform.position, Quaternion.identity);
            projectile.Initialized(owner, direction, targetMask, AbilityTag, true);
        }

        // 애니메이션 지속 시간 기반 딜레이
        //float animLength = owner.GetAnimator().GetCurrentAnimatorStateInfo(0).length;
        //float duration = Mathf.Max(Duration);
        float delay = Duration / Mathf.Max(owner.attribute.attackSpeed, 0.01f); // 어택 스피드가 0인경우는 없지만 혹시 모르니

        yield return new WaitForSeconds(delay);

        EndAbility();
    }

}