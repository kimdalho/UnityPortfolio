#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(DungeonGenerator))]
public class DungeonGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        GUILayout.Space(10);

        if (GUILayout.Button("모든 던전 생성", GUILayout.Height(30)))
        {
            DungeonGenerator dungeonGener = (DungeonGenerator)target;
            dungeonGener.BuildAllDungeonsInEditor();
        }
    }
}
#endif