#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
[CustomEditor(typeof(GridRow))]
public class GridRowEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        GUILayout.Space(10);

        if (GUILayout.Button("자식 위치 정렬", GUILayout.Height(30)))
        {
            GridRow positioner = (GridRow)target;
            positioner.RepositionChildren();
        }

        if (GUILayout.Button("리스트 초기화", GUILayout.Height(30)))
        {
            GridRow positioner = (GridRow)target;
            positioner.SetList();
        }

        if (GUILayout.Button("벽 생성", GUILayout.Height(30)))
        {
            GridRow positioner = (GridRow)target;
            positioner.CreateWall();
        }
    }
}
#endif