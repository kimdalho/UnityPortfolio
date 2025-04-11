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

        if (GUILayout.Button("자식 위치 정렬", GUILayout.Height(30)))
        {
            RoomGrid positioner = (RoomGrid)target;
            positioner.RepositionChildren();
        }

        if (GUILayout.Button("리스트 초기화", GUILayout.Height(30)))
        {
            RoomGrid positioner = (RoomGrid)target;
            positioner.SetList();
        }
    }
}
#endif