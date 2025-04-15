
#if UNITY_EDITOR

using System.Collections;
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

    private void GenerateRandomNodes(int count, int itemRoomCount, int monsterRoomCount)
    {
        List<Vector2> placedPositions = new List<Vector2>();
        List<RoomNode> placedNodes = new List<RoomNode>();

        Vector2 startPosition = Vector2.zero;
        placedPositions.Add(startPosition);

        var firstNode = graphView.CreateRoomNode(startPosition, eRoomType.Start);
        placedNodes.Add(firstNode);

        System.Random rand = new System.Random();

        int attempts = 0;
        int maxAttempts = 1000;
        var baseNode = firstNode;
        var basePos = baseNode.GetPosition().position;

        while (placedNodes.Count < count && attempts < maxAttempts)
        {
            attempts++;
            
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

                // 새로운 RoomNode 생성
                var newNode = new RoomNode(newPos);


                placedPositions.Add(newPos);
                placedNodes.Add(newNode);

               
                // 자동 연결
                string fromPort = string.Format("Output {0}", GetPortName(dir));
                string toPort = string.Format("Input {0}", GetPortName(-dir));
                LinkNodes(baseNode, fromPort, newNode, toPort);

                string reverseFromPort = string.Format("Output {0}", GetPortName(-dir));
                string reverseToPort = string.Format("Input {0}", GetPortName(dir));
                LinkNodes(newNode, reverseFromPort, baseNode, reverseToPort);

                baseNode = newNode;
                basePos = newPos;

                placed = true;
                break;
            }

            if (!placed)
            {
                // 한 번도 배치되지 않은 경우 루프 탈출 방지
                Debug.LogWarning($"배치할 수 없는 위치가 계속 나옴, 루프 종료.");
                break;
            }
        }

        if (attempts >= maxAttempts)
        {
            Debug.LogError("노드를 생성하는데 너무 많은 시간이 걸림: 최대 시도 횟수 초과");
            return;
        }

        AssignRoomTypes(placedNodes, itemRoomCount, monsterRoomCount);
    }

    private void SelectRandomAsRoomType(List<RoomNode> placedNodes,int count, eRoomType targetType)
    {
        var matched = placedNodes;
        if (matched.Count >= count)
        {
            System.Random rand = new System.Random();
            List<RoomNode> targetList = matched
                .Where(x => x.RoomType == eRoomType.Empty) // 조건 필터링
                .OrderBy(x => rand.Next())                // 랜덤 셔플
                .Take(count)                              // 갯수 선택
                .ToList();                                // 리스트로 변환

            foreach (var roomNode in targetList)
            {
                graphView.CreateRoomNode(roomNode, targetType);
            }           
        }
        else
        {
            Debug.Log("룸의 최대 갯수보다 선택대상의 갯수가 작다");
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

        var item_inputField = new IntegerField("Item Count");
        item_inputField.value = 1;

        var monster_inputField = new IntegerField("Monster Count");
        monster_inputField.value = 1;

        var temproom_inputField = new IntegerField("Temp Count");
        temproom_inputField.value = 2;

        var button = new Button(() => {
            int totalCount = 2 + item_inputField.value + monster_inputField.value + temproom_inputField.value;
            int count = Mathf.Clamp(totalCount, 3, 15);
            
            
            GenerateRandomNodes(count, item_inputField.value, monster_inputField.value);
            
        })

        { text = "생성" };

        toolbar.Add(item_inputField);
        toolbar.Add(monster_inputField);
        toolbar.Add(temproom_inputField);
        toolbar.Add(button);
    }

    public void AssignRoomTypes(List<RoomNode> rooms, int n1, int n2)
    {
        if (rooms.Count < n1 + n2 + 2)
        {
            Debug.LogError("방 개수가 부족합니다.");
            return;
        }

        System.Random rand = new System.Random();

        // 중간 인덱스 리스트 추출 (0:Start, 마지막:Boss 제외)
        var middleIndices = Enumerable.Range(1, rooms.Count - 2).OrderBy(x => rand.Next()).ToList();

        var itemIndices = middleIndices.Take(n1);
        var monsterIndices = middleIndices.Skip(n1).Take(n2);

        for (int i = 0; i < rooms.Count; i++)
        {
            if (i == 0)
                continue;
            else if (i == rooms.Count - 1)
                graphView.CreateRoomNode(rooms[i], eRoomType.Boss);
            else if (itemIndices.Contains(i))
                graphView.CreateRoomNode(rooms[i], eRoomType.Item);
            else if (monsterIndices.Contains(i))
                graphView.CreateRoomNode(rooms[i], eRoomType.Monster);
            else
                graphView.CreateRoomNode(rooms[i], eRoomType.Empty);
        }
    }
}

#endif