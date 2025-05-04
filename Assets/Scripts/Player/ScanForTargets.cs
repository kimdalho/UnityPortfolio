using System;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

public class ScanForTargets : MonoBehaviour
{
    [SerializeField]
    public CinemachineTargetGroup m_TargetGroup;
    [SerializeField]
    private Player player;

    private float timer;
    private Collider[] buffer = new Collider[20]; // 최대 20개까지 감지
    private List<IcanGetHead> currentTargets = new List<IcanGetHead>();
    private IcanGetHead _lookatMonster;

    public Transform lookatMonster
    {
        get
        {
            if (_lookatMonster == null || _lookatMonster.GetDead())
                return null;
            return _lookatMonster.GetHead();
        }
    }

    private void Start()
    {
        player = GameManager.instance.GetPlayer();
    }

    private readonly int condition = 0;
    private readonly int TargetIndex = 0;

    private void Update()
    {
        if (m_TargetGroup.Targets.Count > condition)
        {
            Transform target = m_TargetGroup.Targets[TargetIndex].Object;
            bool targetDead = target.GetComponent<IcanGetHead>().GetDead();
            if(targetDead)
            {
                ResetTarget();
            }
        }
    }

    public void SetPlayerTarget(Monster monster)
    {
        if (monster != null)
        {
            Transform monsterHead = monster.GetHead();  // 몬스터의 머리 Transform을 가져옴
            if (m_TargetGroup.FindMember(monsterHead) == -1)  // 아직 그룹에 없으면
            {                             
                if (m_TargetGroup.Targets.Count > condition)
                {
                    Transform targetToRemove = m_TargetGroup.Targets[TargetIndex].Object;
                    m_TargetGroup.RemoveMember(targetToRemove);                                                                
                }
                m_TargetGroup.AddMember(monsterHead, 0.3f, 0.5f);  // 타겟 그룹에 추가
                _lookatMonster = monsterHead.GetComponent<IcanGetHead>();  // 해당 몬스터를 _lookatMonster로 설정     

            }
            
        }
    }

    public void ResetTarget()
    {
        if(m_TargetGroup.Targets.Count > condition)
        {
            Transform targetToRemove = m_TargetGroup.Targets[TargetIndex].Object;
            m_TargetGroup.RemoveMember(targetToRemove);
            _lookatMonster = null;
            player.gameplayTagSystem.RemoveTag(eTagType.Player_State_HasAttackTarget);
        }
    }
}

