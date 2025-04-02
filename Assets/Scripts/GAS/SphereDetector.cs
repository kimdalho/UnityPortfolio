using UnityEngine;

public class SphereDetector : MonoBehaviour
{
    /// <summary>
    /// 주어진 중심 좌표와 반지름으로 구체를 생성하고, 닿은 객체를 반환
    /// </summary>
    /// <param name="center">구체의 중심 좌표</param>
    /// <param name="radius">구체의 반지름</param>
    /// <param name="layerMask">검출할 레이어 (기본값: 모든 레이어)</param>
    /// <returns>구체에 닿은 Collider 배열</returns>
    public static Collider[] DetectObjectsInSphere(Vector3 center, float radius, LayerMask layerMask = default)
    {
        // 구체와 충돌한 모든 객체 가져오기
        Collider[] hitColliders = Physics.OverlapSphere(center, radius, layerMask);

        // 디버그용으로 구체의 테두리를 그리기
        DrawDebugSphere(center, radius);

        return hitColliders;
    }

    /// <summary>
    /// Debug.DrawLine을 이용해 구체의 테두리를 시각적으로 표시
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
