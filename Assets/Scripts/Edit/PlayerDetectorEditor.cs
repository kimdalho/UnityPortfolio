#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Player))]
public class PlayerDetectorEditor : Editor
{
    void OnSceneGUI()
    {
        Player detector = (Player)target;

        // �߽� ��ġ�� ����
        Vector3 center = detector.transform.position;
        Color fillColor = detector.gizmoColor;
        fillColor.a = 0.2f; // ���� ����

        // ������ ä���s
        Handles.color = fillColor;
        Handles.DrawSolidDisc(center, Vector3.up, detector.scanRadius);

        // �ܰ���
        Handles.color = detector.gizmoColor;
        Handles.DrawWireDisc(center, Vector3.up, detector.scanRadius);
    }
}
#endif