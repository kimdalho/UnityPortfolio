#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(RoomGrid))]
public class RoomGridEdior : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        GUILayout.Space(10);

        if (GUILayout.Button("�ڽ� ��ġ ����", GUILayout.Height(30)))
        {
            RoomGrid positioner = (RoomGrid)target;
            positioner.RepositionChildren();
        }

        if (GUILayout.Button("����Ʈ �ʱ�ȭ", GUILayout.Height(30)))
        {
            RoomGrid positioner = (RoomGrid)target;
            positioner.SetList();
        }
    }
}
#endif