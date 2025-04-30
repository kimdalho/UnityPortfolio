using System;
using System.Collections.Generic;
using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class ScanForTargets : MonoBehaviour
{
    [SerializeField]
    public CinemachineTargetGroup m_TargetGroup;
    [SerializeField]
    private Player player;

    public float scanRadius = 5f;
    public LayerMask targetLayer;
    public float scanInterval = 0.2f;

    private float timer;
    private Collider[] buffer = new Collider[20]; // 최대 20개까지 감지
    private List<IcanGetHead> currentTargets  = new List<IcanGetHead>();
    public Transform lookatMonster
    {
        get
        {
            if(_lookatMonster == null || _lookatMonster.GetDead() )
                return null;
            return _lookatMonster.GetHead();
        }
    }

    private IcanGetHead _lookatMonster;


    private void Start()
    {
        player = GameManager.instance.GetPlayer();
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= scanInterval)
        {
            timer = 0f;
            Scan();           
        }
    }

    void Scan()
    {
        int count = Physics.OverlapSphereNonAlloc(transform.position, scanRadius, buffer, targetLayer);

        // 현재 감지된 타겟 새로 수집
        HashSet<IcanGetHead> newTargets = new HashSet<IcanGetHead>();

        for (int i = 0; i < count; i++)
        {
            IcanGetHead targetHead = buffer[i].GetComponent<IcanGetHead>();
            if (targetHead == null) continue;

            if(!player.gameplayTagSystem.HasTag(eTagType.Player_State_HasAttackTarget))
                player.gameplayTagSystem.AddTag(eTagType.Player_State_HasAttackTarget);

            newTargets.Add(targetHead);

            if (!currentTargets.Contains(targetHead))
            {
                currentTargets.Add(targetHead);

                Transform headTransform = targetHead.GetHead();
                if (m_TargetGroup.FindMember(headTransform) == -1 && m_TargetGroup.Targets.Count < 3)
                {                    
                    m_TargetGroup.AddMember(headTransform, 1f, 0.5f);
                    _lookatMonster = targetHead;
                }
            }
        }

        // currentTargets에서 사라진 애들 + 죽은대상 제거
        List<IcanGetHead> toRemove = new List<IcanGetHead>();

        foreach (var target in currentTargets)
        {
            if (!newTargets.Contains(target) || target.GetDead())
            {
                toRemove.Add(target);
            }
        }



        foreach (var target in toRemove)
        {
            Transform headTransform = target.GetHead();
            if (m_TargetGroup.FindMember(headTransform) >= 0)
            {
                m_TargetGroup.RemoveMember(headTransform);
                player.gameplayTagSystem.RemoveTag(eTagType.Player_State_HasAttackTarget);
            }

            currentTargets.Remove(target);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0f, 1f, 0f, 0.25f); 
        Gizmos.DrawSphere(transform.position, scanRadius);
    }
}

