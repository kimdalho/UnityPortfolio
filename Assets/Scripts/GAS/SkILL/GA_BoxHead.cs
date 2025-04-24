using System.Collections;
using UnityEngine;

public class GA_BoxHead : GameAbility
{
    private int baseFireCount = 1;
    private ProjectileType projectileType = ProjectileType.Bullet;

    private int increaseCount = 0;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        Duration = 3f;
        AbilityTag = eTagType.boxhead;
    }

    protected override IEnumerator ExecuteAbility()
    {
        yield return null; // 임시

        var _bodyTrans = owner.transform.GetChild(0).GetChild(owner.transform.GetChild(0).childCount - 1).GetChild(0).GetChild(0);
        owner.gameplayTagSystem.AddTag(AbilityTag);

        while (true)
        {
            // 획득 시 바로 발사 X
            yield return new WaitForSeconds(Duration);

            // 투사체 증가 패시브 적용
            increaseCount = owner.gameplayTagSystem.HasTag(eTagType.clownhair) ? GA_ClownHair.IncreaseCount : baseFireCount;

            owner.gameplayTagSystem.RemoveTag(AbilityTag);

            for (int i = 0; i < increaseCount; i++)
            {
                InitProjectile(_bodyTrans, owner.transform.forward, false);
                yield return null;
            }

            increaseCount = 0;
        }

        EndAbility();
    }

    private void InitProjectile(Transform bodyTrans, Vector3 dir, bool isAtkSpeedAdd = false)
    {
        var _return = ProjectileFactory.Instance.GetProjectile(projectileType, bodyTrans.position + Vector3.up * 0.3f, Quaternion.identity);
        _return.Initialized(owner, dir.normalized, targetMask, AbilityTag, isAtkSpeedAdd);
    }
}