using UnityEngine;

public class SphereDetector : MonoBehaviour
{
    /// <summary>
    /// �־��� �߽� ��ǥ�� ���������� ��ü�� �����ϰ�, ���� ��ü�� ��ȯ
    /// </summary>
    /// <param name="center">��ü�� �߽� ��ǥ</param>
    /// <param name="radius">��ü�� ������</param>
    /// <param name="layerMask">������ ���̾� (�⺻��: ��� ���̾�)</param>
    /// <returns>��ü�� ���� Collider �迭</returns>
    public static Collider[] DetectObjectsInSphere(Vector3 center, float radius, LayerMask layerMask = default)
    {
        // ��ü�� �浹�� ��� ��ü ��������
        Collider[] hitColliders = Physics.OverlapSphere(center, radius, layerMask);

        // ����׿����� ��ü�� �׵θ��� �׸���
        DrawDebugSphere(center, radius);

        return hitColliders;
    }

    /// <summary>
    /// Debug.DrawLine�� �̿��� ��ü�� �׵θ��� �ð������� ǥ��
    /// </summary>
    private static void DrawDebugSphere(Vector3 center, float radius)
    {
        int segments = 36;
        float angleStep = 360f / segments;
        Vector3 prevPointX = center + new Vector3(radius, 0, 0);
        Vector3 prevPointY = center + new Vector3(0, radius, 0);
        Vector3 prevPointZ = center + new Vector3(0, 0, radius);

        for (int i = 1; i <= segments; i++)
        {
            float angle = i * angleStep * Mathf.Deg2Rad;
            Vector3 nextPointX = center + new Vector3(Mathf.Cos(angle) * radius, 0, Mathf.Sin(angle) * radius);
            Vector3 nextPointY = center + new Vector3(Mathf.Cos(angle) * radius, Mathf.Sin(angle) * radius, 0);
            Vector3 nextPointZ = center + new Vector3(0, Mathf.Cos(angle) * radius, Mathf.Sin(angle) * radius);

            Debug.DrawLine(prevPointX, nextPointX, Color.red, 0.5f);
            Debug.DrawLine(prevPointY, nextPointY, Color.red, 0.5f);
            Debug.DrawLine(prevPointZ, nextPointZ, Color.red, 0.5f);

            prevPointX = nextPointX;
            prevPointY = nextPointY;
            prevPointZ = nextPointZ;
        }
    }
}
