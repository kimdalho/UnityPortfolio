
#if UNITY_EDITOR

using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public class DungeonGraphEditorWindow : EditorWindow
{
    private DungeonGraphView graphView;

    [MenuItem("Tools/Dungeon Graph Editor")]
    public static void OpenWindow()
    {
        DungeonGraphEditorWindow window = GetWindow<DungeonGraphEditorWindow>();
        window.titleContent = new GUIContent("Dungeon Graph Editor");
    }

    private void OnEnable()
    {
        ConstructGraphView();
        AddGenerateButton();
    }

    private void ConstructGraphView()
    {
        graphView = new DungeonGraphView
        {
            name = "Dungeon Graph"
        };
        graphView.StretchToParentSize();
        rootVisualElement.Add(graphView);
    }


    private void OnDisable()
    {
        rootVisualElement.Remove(graphView);
    }

    private void SaveAsset()
    {
        var data = graphView.SaveGraphData();

        string path = EditorUtility.SaveFilePanelInProject("Save Dungeon Graph", "NewDungeonGraph", "asset", "Save your dungeon graph");
        if (string.IsNullOrEmpty(path)) return;

        AssetDatabase.CreateAsset(data, path);
        AssetDatabase.SaveAssets();
    }

    private void GenerateRandomNodes(int count)
    {
        List<Vector2> placedPositions = new List<Vector2>();
        List<RoomNode> placedNodes = new List<RoomNode>();

        Vector2 startPosition = Vector2.zero;
        placedPositions.Add(startPosition);

        var firstNode = graphView.CreateRoomNode(startPosition, eRoomType.Monster);
        placedNodes.Add(firstNode);

        System.Random rand = new System.Random();

        while (placedNodes.Count < count)
        {
            var baseNode = placedNodes[rand.Next(placedNodes.Count)];
            var basePos = baseNode.GetPosition().position;

            bool placed = false;

            var directions = new List<Vector2>
        {
            new Vector2(100, 0),   // →
            new Vector2(-100, 0),  // ←
            new Vector2(0, 100),   // ↑
            new Vector2(0, -100)   // ↓
        };
            directions = directions.OrderBy(x => rand.Next()).ToList();

            foreach (var dir in directions)
            {
                Vector2 newPos = basePos + dir;
                if (placedPositions.Contains(newPos)) continue;

                var newNode = graphView.CreateRoomNode(newPos, eRoomType.Monster);
                placedPositions.Add(newPos);
                placedNodes.Add(newNode);

                // 자동 연결
                string fromPort = string.Format("Output {0}", GetPortName(dir));              // 기준 노드의 방향
                string toPort = string.Format("Input {0}", GetPortName(-dir)) ;               // 새 노드의 반대 방향

                LinkNodes(baseNode, fromPort, newNode, toPort);

                string reverseFromPort = string.Format("Output {0}", GetPortName(-dir));       // newNode 방향 그대로
                string reverseToPort = string.Format("Input {0}", GetPortName(dir));        // baseNode 반대방향
                LinkNodes(newNode, reverseFromPort, baseNode, reverseToPort);

                placed = true;
                break;
            }

            if (!placed) continue;
        }
    }

    private string GetPortName(Vector2 dir)
    {
        if (dir == new Vector2(100, 0)) return "Right";
        if (dir == new Vector2(-100, 0)) return "Left";
        if (dir == new Vector2(0, 100)) return "Top";
        if (dir == new Vector2(0, -100)) return "Bottom";
        return "";
    }

    public void LinkNodes(RoomNode fromNode, string fromPortName, RoomNode toNode, string toPortName)
    {
        var fromPort = fromNode.GetOutputPort(fromPortName);
        var toPort = toNode.GetInputPort(toPortName);

        var edge = fromPort.ConnectTo(toPort);
        graphView.AddElement(edge);
    }

    private void AddGenerateButton()
    {
        Toolbar toolbar = new Toolbar();

        Button addRoomButton = new Button(() =>
        {
            graphView.CreateRoomNode(new Vector2(100, 200));
        })
        { text = "Add Room" };

        toolbar.Add(addRoomButton);
        rootVisualElement.Add(toolbar);

        Button saveButton = new Button(SaveAsset) { text = "Save Dungeon" };
        toolbar.Add(saveButton);

        var inputField = new IntegerField("Node Count");
        inputField.value = 5;

        var button = new Button(() => {
            int count = Mathf.Clamp(inputField.value, 3, 10);
            GenerateRandomNodes(count);
        })
        { text = "랜덤 노드 자동 생성" };

        toolbar.Add(inputField);
        toolbar.Add(button);
    }
}

#endif