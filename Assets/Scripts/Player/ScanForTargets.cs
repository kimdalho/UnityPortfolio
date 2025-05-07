using System;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

public class ScanForTargets : MonoBehaviour
{
    [SerializeField]
    public CinemachineTargetGroup m_TargetGroup;    
    
    private List<ILockOnTarget> currentTargets = new List<ILockOnTarget>();
    private ILockOnTarget _lookatMonster;
    private readonly int condition = 0;
    private readonly int TargetIndex = 0;

    public static Action<Transform> OnSetLockOnTarget;

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
            bool targetDead = target.GetComponent<ILockOnTarget>().GetDead();
            if(targetDead)
            {
                ResetTarget();
            }
        }
    }

    public void SetPlayerTarget(ILockOnTarget monster)
    {
        if (monster != null)
        {
            Transform monsterHead = monster.GetLockOnTransform();  // 몬스터의 머리 Transform을 가져옴
            if (m_TargetGroup.FindMember(monsterHead) == -1)  // 아직 그룹에 없으면
            {                             
                if (m_TargetGroup.Targets.Count > condition)
                {
                    Transform targetToRemove = m_TargetGroup.Targets[TargetIndex].Object;
                    m_TargetGroup.RemoveMember(targetToRemove);                                                                
                }
                m_TargetGroup.AddMember(monsterHead, 0.3f, 0.5f);  // 타겟 그룹에 추가
                _lookatMonster = monsterHead.GetComponent<ILockOnTarget>();  // 해당 몬스터를 _lookatMonster로 설정     
                
                OnSetLockOnTarget?.Invoke(monsterHead);
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

