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

        if (GUILayout.Button("钱傅 鞘靛 积己", GUILayout.Height(30)))
        {
            DungeonGenerator dungeonGener = (DungeonGenerator)target;
            dungeonGener.BuildPoolingRooms();
        }

        if (GUILayout.Button("葛电 带傈 积己", GUILayout.Height(30)))
        {
            DungeonGenerator dungeonGener = (DungeonGenerator)target;
            dungeonGener.BuildAllDungeonsInEditor();
        }

        
    }
}
#endif