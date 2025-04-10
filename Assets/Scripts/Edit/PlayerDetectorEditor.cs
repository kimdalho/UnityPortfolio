#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Player))]
public class PlayerDetectorEditor : Editor
{
    void OnSceneGUI()
    {
        Player detector = (Player)target;

        // 중심 위치와 색상
        Vector3 center = detector.transform.position;
        Color fillColor = detector.gizmoColor;
        fillColor.a = 0.2f; // 투명도 조절

        // 반투명 채우기s
        Handles.color = fillColor;
        Handles.DrawSolidDisc(center, Vector3.up, detector.scanRadius);

        // 외곽선
        Handles.color = detector.gizmoColor;
        Handles.DrawWireDisc(center, Vector3.up, detector.scanRadius);
    }
}
#endif