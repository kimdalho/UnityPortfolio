using System.Collections;
using UnityEngine;

public abstract class AttackAbility : GameAbility
{
    protected bool IsPoison;
    protected override bool IsOnCooldown 
    { 
        get => base.IsOnCooldown;
        set
        {
            base.IsOnCooldown = value;
            // Owner가 몬스터일 경우 AtkCool 설정
            if (owner is Monster) (owner as Monster).IsAtkCool = value;
        }
    }

    protected override IEnumerator ExecuteAbility()
    {
        // Roostershead 태그가 활성화 되어 있다면 투사체에 독 적용 (투사체 외의 공격도 독 데미지 적용을 할 가능성이 있으므로 여기서 적용)
        IsPoison = owner.gameplayTagSystem.HasTag(eTagType.Roostershead);

        if (owner.gameplayTagSystem.HasTag(eTagType.beartorso) && GA_BearTorso.ReturnSuccessResult())
        {
            FanShapeFire(owner.transform, ProjectileType.Bullet, 40f, 2);
        }

        yield break;
    }

    /// <summary>
    /// 해당 어빌리티의 맞는 패시브 어빌리티가 적용될 수 있도록 어빌리티 태그를 적용시킴.
    /// </summary>
    /// <returns></returns>
    protected virtual IEnumerator ApplyTag()
    {
        // 해당 Ability의 Tag를 추가
        owner.gameplayTagSystem.AddTag(AbilityTag);

        // 해당 태그를 다른 Ability들이 감지할 수 있도록 한 프레임 기다림.
        yield return null;

        // 모든 감지가 끝난 이후 추가되었던 Tag는 '재감지'가 되지 않도록 Tag를 삭제
        owner.gameplayTagSystem.RemoveTag(AbilityTag);
    }

    protected virtual void InitProjectile(Transform baseTrans, ProjectileType projectileType, Vector3 dir, bool isAtkSpeedAdd = false)
    {
        var _return = ProjectileFactory.Instance.GetProjectile(projectileType, baseTrans.position, Quaternion.identity);
        _return.Initialized(owner, dir.normalized, targetMask, AbilityTag, isAtkSpeedAdd);
    }

    protected virtual void FanShapeFire(Transform baseTrans, ProjectileType projectileType, float angleRange, int count)
    {
        if (count.Equals(1))
        {
            InitProjectile(baseTrans, projectileType, owner.transform.forward, true);
            return;
        }

        var _startDir = Quaternion.AngleAxis(-angleRange, Vector3.up) * owner.transform.forward;
        var _endDir = Quaternion.AngleAxis(angleRange, Vector3.up) * owner.transform.forward;

        for (int i = 0; i < count; i++)
        {
            var _dir = Vector3.Lerp(_startDir, _endDir, i / (float)(count - 1));
            InitProjectile(baseTrans, projectileType, _dir, true);
        }
    }
}