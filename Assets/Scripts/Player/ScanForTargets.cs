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
    private Collider[] buffer = new Collider[20]; // �ִ� 20������ ����
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
            Transform monsterHead = monster.GetHead();  // ������ �Ӹ� Transform�� ������
            if (m_TargetGroup.FindMember(monsterHead) == -1)  // ���� �׷쿡 ������
            {                             
                if (m_TargetGroup.Targets.Count > condition)
                {
                    Transform targetToRemove = m_TargetGroup.Targets[TargetIndex].Object;
                    m_TargetGroup.RemoveMember(targetToRemove);                                                                
                }
                m_TargetGroup.AddMember(monsterHead, 0.3f, 0.5f);  // Ÿ�� �׷쿡 �߰�
                _lookatMonster = monsterHead.GetComponent<IcanGetHead>();  // �ش� ���͸� _lookatMonster�� ����     

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

