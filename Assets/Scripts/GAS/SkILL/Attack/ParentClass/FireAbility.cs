using System.Collections;
using UnityEngine;

/// <summary>
/// ����ü�� �߻��ϴ� ��ų
/// </summary>
public abstract class FireAbility : AttackAbility
{
    [SerializeField] protected int baseFireCount = 1; // �⺻ �߻�ü ��
    [SerializeField] protected ProjectileType projectileType;
    [SerializeField] protected float delayAtkTime = 0.3f;

    protected int FireCount => increaseCount; // ������ �߻�ü ��

    private int increaseCount = 0;

    public void ChangeProjectileType(ProjectileType newType) => projectileType = newType;

    protected override IEnumerator ExecuteAbility()
    {
        yield return base.ExecuteAbility();

        // Clown Hair�� ������ ������ ����ü 3���� ����
        increaseCount = owner.gameplayTagSystem.HasTag(eTagType.clownhair) ? GA_ClownHair. IncreaseCount : baseFireCount;

        // BoxBody�� ������ �ִٸ� ���� ����ü �������� 2�� ������ ���·� �߻�
        if (owner.gameplayTagSystem.HasTag(eTagType.boxbody))
        {
            increaseCount += 2;
            owner.gameplayTagSystem.RemoveTag(eTagType.boxbody);
        }

        yield break;
    }

    public override void EndAbility()
    {
        base.EndAbility();
        increaseCount = 0; // �߻�ü ������ ����
    }
}
