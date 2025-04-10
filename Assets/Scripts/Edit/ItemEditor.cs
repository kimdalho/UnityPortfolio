
#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;

public class ItemEditor : EditorWindow
{
    GameObject itemPrefab; // 생성할 아이템 프리팹

    [MenuItem("Tools/Item Spawner")]
    public static void ShowWindow()
    {
        GetWindow<ItemEditor>("Item Spawner");
    }

    void OnGUI()
    {
        GUILayout.Label("Item Spawner Tool", EditorStyles.boldLabel);

        itemPrefab = (GameObject)EditorGUILayout.ObjectField("Item Prefab", itemPrefab, typeof(GameObject), false);

        if (itemPrefab == null)
        {
            EditorGUILayout.HelpBox("아이템 프리팹을 설정하세요.", MessageType.Warning);
            return;
        }

        if (GUILayout.Button("Spawn Item in Scene"))
        {
            SpawnItem();
        }
    }

    void SpawnItem()
    {
        if (itemPrefab != null)
        {
            GameObject newItem = (GameObject)PrefabUtility.InstantiatePrefab(itemPrefab);
            if (SceneView.lastActiveSceneView != null)
            {
                Vector3 spawnPosition = SceneView.lastActiveSceneView.pivot;
                newItem.transform.position = spawnPosition;
                Selection.activeGameObject = newItem;
            }
        }
    }
}
#endif