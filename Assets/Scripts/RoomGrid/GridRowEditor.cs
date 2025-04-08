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

        if (GUILayout.Button("�ڽ� ��ġ ����", GUILayout.Height(30)))
        {
            GridRow positioner = (GridRow)target;
            positioner.RepositionChildren();
        }

        if (GUILayout.Button("����Ʈ �ʱ�ȭ", GUILayout.Height(30)))
        {
            GridRow positioner = (GridRow)target;
            positioner.SetList();
        }

        if (GUILayout.Button("�� ����", GUILayout.Height(30)))
        {
            GridRow positioner = (GridRow)target;
            positioner.CreateWall();
        }
    }
}
#endif