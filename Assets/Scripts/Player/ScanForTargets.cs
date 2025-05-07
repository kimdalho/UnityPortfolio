using System;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

public class ScanForTargets : MonoBehaviour
{
    [SerializeField]
    public CinemachineTargetGroup m_TargetGroup;    
    
    private List<ILockOnService> currentTargets = new List<ILockOnService>();
    private ILockOnService _lookatMonster;
    private readonly int condition = 0;
    private readonly int TargetIndex = 0;

    public Transform lookatMonster
    {
        get
        {
            if (_lookatMonster == null || _lookatMonster.GetDead())
                return null;
            return _lookatMonster.GetLockOnTransform();
        }
    }


    private void Update()
    {
        if (m_TargetGroup.Targets.Count > condition)
        {
            Transform target = m_TargetGroup.Targets[TargetIndex].Object;
            bool targetDead = target.GetComponent<ILockOnService>().GetDead();
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
            Transform monsterHead = monster.GetLockOnTransform();  // ������ �Ӹ� Transform�� ������
            if (m_TargetGroup.FindMember(monsterHead) == -1)  // ���� �׷쿡 ������
            {                             
                if (m_TargetGroup.Targets.Count > condition)
                {
                    Transform targetToRemove = m_TargetGroup.Targets[TargetIndex].Object;
                    m_TargetGroup.RemoveMember(targetToRemove);                                                                
                }
                m_TargetGroup.AddMember(monsterHead, 0.3f, 0.5f);  // Ÿ�� �׷쿡 �߰�
                _lookatMonster = monsterHead.GetComponent<ILockOnService>();  // �ش� ���͸� _lookatMonster�� ����     

            }
            
        }
    }

    public bool ResetTarget()
    {
        if(m_TargetGroup.Targets.Count > condition)
        {
            Transform targetToRemove = m_TargetGroup.Targets[TargetIndex].Object;
            m_TargetGroup.RemoveMember(targetToRemove);
            _lookatMonster = null;  
            return true;
        }
        return false;
    }
}

