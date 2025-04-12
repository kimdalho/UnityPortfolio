#if UNITY_EDITOR
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

public class RoomPlacerEditor : EditorWindow
{
    private enum Tab { RoomPlacer, ExportCSV }
    private Tab currentTab;

    private GameObject roomPrefab;
    private const int gridSize = 5;
    private float horizontalSpacing = 10f;
    private float verticalSpacing = 10f;
    private int centerX = 2;
    private int centerY = 2;

    [MenuItem("Tools/Room Placer")]
    public static void ShowWindow()
    {
        GetWindow<RoomPlacerEditor>("Room Editor");
    }

    private void OnGUI()
    {
        // 탭 선택
        currentTab = (Tab)GUILayout.Toolbar((int)currentTab, new string[] { "Room Placer", "Export CSV" });

        GUILayout.Space(10);

        switch (currentTab)
        {
            case Tab.RoomPlacer:
                DrawRoomPlacerTab();
                break;
            case Tab.ExportCSV:
                DrawExportCSVTab();
                break;
        }
    }

    private void DrawRoomPlacerTab()
    {
        roomPrefab = (GameObject)EditorGUILayout.ObjectField("Room Prefab", roomPrefab, typeof(GameObject), false);
        horizontalSpacing = EditorGUILayout.FloatField("가로 간격 (X)", horizontalSpacing);
        verticalSpacing = EditorGUILayout.FloatField("세로 간격 (Z)", verticalSpacing);
        centerX = EditorGUILayout.IntSlider("중심 X 좌표", centerX, 0, gridSize - 1);
        centerY = EditorGUILayout.IntSlider("중심 Y 좌표", centerY, 0, gridSize - 1);

        if (roomPrefab == null)
        {
            EditorGUILayout.HelpBox("Room Prefab을 드래그해서 넣어주세요.", MessageType.Warning);
            return;
        }

        for (int y = gridSize - 1; y >= 0; y--)
        {
            GUILayout.BeginHorizontal();
            for (int x = 0; x < gridSize; x++)
            {
                if (GUILayout.Button($"({x},{y})", GUILayout.Width(60), GUILayout.Height(40)))
                {
                    PlaceRoom(x, y);
                }
            }
            GUILayout.EndHorizontal();
        }
    }

    private void PlaceRoom(int x, int y)
    {
        float posX = (x - centerX) * horizontalSpacing;
        float posZ = (y - centerY) * verticalSpacing;

        Vector3 position = new Vector3(posX, 0, posZ);
        GameObject room = (GameObject)PrefabUtility.InstantiatePrefab(roomPrefab);
        room.transform.position = position;

        Undo.RegisterCreatedObjectUndo(room, "Place Room");
    }

    private void DrawExportCSVTab()
    {
        if (GUILayout.Button("Export Room Map to CSV", GUILayout.Height(40)))
        {
            ExportRoomMapToCSV();
        }
    }

    private void ExportRoomMapToCSV()
    {
        StringBuilder csv = new StringBuilder();
        csv.AppendLine("RoomType,X,Z");

        GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();
        foreach (GameObject obj in allObjects)
        {
            if (PrefabUtility.GetPrefabAssetType(obj) == PrefabAssetType.NotAPrefab)
                continue;

            if (!obj.name.ToLower().Contains("room"))
                continue;

            Vector3 pos = obj.transform.position;
            csv.AppendLine($"Room,{pos.x},{pos.z}");
        }

        string path = Application.dataPath + "/MapData.csv";
        File.WriteAllText(path, csv.ToString(), Encoding.UTF8);

        Debug.Log("CSV 저장 완료: " + path);
        AssetDatabase.Refresh();
    }
}

#endif