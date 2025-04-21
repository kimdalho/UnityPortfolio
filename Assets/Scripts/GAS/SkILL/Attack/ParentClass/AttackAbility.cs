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
            // Owner�� ������ ��� AtkCool ����
            if (owner is Monster) (owner as Monster).IsAtkCool = value;
        }
    }

    protected override IEnumerator ExecuteAbility()
    {
        // Roostershead �±װ� Ȱ��ȭ �Ǿ� �ִٸ� ����ü�� �� ���� (����ü ���� ���ݵ� �� ������ ������ �� ���ɼ��� �����Ƿ� ���⼭ ����)
        IsPoison = owner.gameplayTagSystem.HasTag(eTagType.Roostershead);

        if (owner.gameplayTagSystem.HasTag(eTagType.beartorso) && GA_BearTorso.ReturnSuccessResult())
        {
            FanShapeFire(owner.transform, ProjectileType.Bullet, 40f, 2);
        }

        yield break;
    }

    /// <summary>
    /// �ش� �����Ƽ�� �´� �нú� �����Ƽ�� ����� �� �ֵ��� �����Ƽ �±׸� �����Ŵ.
    /// </summary>
    /// <returns></returns>
    protected virtual IEnumerator ApplyTag()
    {
        // �ش� Ability�� Tag�� �߰�
        owner.gameplayTagSystem.AddTag(AbilityTag);

        // �ش� �±׸� �ٸ� Ability���� ������ �� �ֵ��� �� ������ ��ٸ�.
        yield return null;

        // ��� ������ ���� ���� �߰��Ǿ��� Tag�� '�簨��'�� ���� �ʵ��� Tag�� ����
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