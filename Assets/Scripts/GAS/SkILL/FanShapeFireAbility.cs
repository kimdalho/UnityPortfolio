using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// 부채꼴 모양으로 발사하는 스킬
/// Normal과 Multiple Fire 모두 대응
/// </summary>
public class FanShapeFireAbility : GameAbility
{
    [SerializeField] private int fireCount = 1; // 발사 횟수
    [SerializeField] private float fireAngleRange = 20f;
    [SerializeField] private ProjectileType projectileType;
    [SerializeField] private float delayAtkTime = 0.3f;


    protected override IEnumerator ExecuteAbility()
    {
        Task task = animTask.AnimExecute(AnimState.Attack);
        yield return new WaitForTask(task);

        // Apply Delay
        var _animLength = owner.GetAnimator().GetCurrentAnimatorStateInfo(0).length;
        var _duration = _animLength > Duration ? _animLength : Duration;

        var _delay = _duration / Mathf.Max(owner.attribute.GetCurValue(eAttributeType.AttackSpeed), 0.01f); // 0으로 나누는 것 방지

        yield return new WaitForSeconds(_delay);
        EndAbility();
    }

    public void ChangeProjectileType(ProjectileType newType) => projectileType = newType;

    protected override void OnFireAnimationApply()
    {
        StartCoroutine(CoFireApply());
    }

    public IEnumerator CoFireApply()
    {
        // 발사 횟수가 1인 경우에는 정면으로 발사
        var _range = fireCount.Equals(1) ? 0f : fireAngleRange;

        var _startDir = Quaternion.AngleAxis(-_range, Vector3.up) * owner.transform.forward;
        var _endDir = Quaternion.AngleAxis(_range, Vector3.up) * owner.transform.forward;

        // Owner의 Hand Transform 탐색
        var _armTrans = owner.transform.GetChild(0).GetChild(owner.transform.GetChild(0).childCount - 1);
        for (int i = 0; i < 8; i++) _armTrans = _armTrans.GetChild(0);

        // Detect 혹은 Projectile이 생성되기까지 Delay되는 시간
        yield return new WaitForSeconds(delayAtkTime);

        for (int i = 0; i < fireCount; i++)
        {
            var _dir = Vector3.Lerp(_startDir, _endDir, (i + 1) / (float)fireCount);

            var _projectile = ProjectileFactory.Instance.GetProjectile(projectileType, _armTrans.position, Quaternion.identity);
            _projectile.Initialized(owner, _dir.normalized, targetMask, AbilityTag, true);

            // 한번 에 발사하므로 다음 프레임때 실행되지 않도록
            // yield return null;
        }

    }
}
