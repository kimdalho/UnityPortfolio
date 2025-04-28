using System.Collections.Generic;
using Unity.Cinemachine;
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
    public Transform lookatMonster;

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
            IcanGetHead head = buffer[i].GetComponent<IcanGetHead>();
            if (head == null) continue;

            newTargets.Add(head);

            if (!currentTargets.Contains(head))
            {
                currentTargets.Add(head);

                Transform headTransform = head.GetHead();
                if (m_TargetGroup.FindMember(headTransform) == -1 && m_TargetGroup.Targets.Count < 3)
                {
                    m_TargetGroup.AddMember(headTransform, 1f, 0.5f);
                    lookatMonster = headTransform;
                }
            }
        }

        // currentTargets에서 사라진 애들 제거
        List<IcanGetHead> toRemove = new List<IcanGetHead>();

        foreach (var target in currentTargets)
        {
            if (!newTargets.Contains(target))
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

