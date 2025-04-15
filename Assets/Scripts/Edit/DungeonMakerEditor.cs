#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(DungeonMaker))]
public class DungeonMakerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        GUILayout.Space(10);

        if (GUILayout.Button("던전 생성", GUILayout.Height(30)))
        {
            DungeonMaker positioner = (DungeonMaker)target;
            positioner.Build();
        }
    }
}
#endif