#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Roomgenerator))]
public class RoomGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        GUILayout.Space(10);

        if (GUILayout.Button("»ý¼º", GUILayout.Height(30)))
        {
            Roomgenerator positioner = (Roomgenerator)target;
            positioner.SetData();
        }
        
    }
}
#endif