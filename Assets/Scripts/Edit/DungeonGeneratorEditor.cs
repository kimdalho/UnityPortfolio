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

        if (GUILayout.Button("Ǯ�� �ʵ� ����", GUILayout.Height(30)))
        {
            DungeonGenerator dungeonGener = (DungeonGenerator)target;
            dungeonGener.BuildPoolingRooms();
        }

        if (GUILayout.Button("��� ���� ����", GUILayout.Height(30)))
        {
            DungeonGenerator dungeonGener = (DungeonGenerator)target;
            dungeonGener.BuildAllDungeonsInEditor();
        }

        
    }
}
#endif