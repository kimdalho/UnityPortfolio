#if UNITY_EDITOR

using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class DungeonGraphView : GraphView
{
    public DungeonGraphView()
    {
        styleSheets.Add(Resources.Load<StyleSheet>("DungeonGraphStyle"));
        SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);
        this.AddManipulator(new ContentDragger());
        this.AddManipulator(new SelectionDragger());
        this.AddManipulator(new RectangleSelector());

        GridBackground grid = new GridBackground();
        Insert(0, grid);
        grid.StretchToParentSize();
    }


    public RoomNode CreateRoomNode(Vector2 position, eRoomType roomType = eRoomType.Empty)
    {
        var node = new RoomNode
        {
            RoomType = roomType
        };

        node.title = roomType.ToString(); // ��� ��ܿ� Ÿ�� ǥ��
        node.SetPosition(new Rect(position, new Vector2(200, 150)));

        // RoomType ��Ӵٿ� �߰�
        var typeField = new EnumField(node.RoomType);
        typeField.RegisterValueChangedCallback(evt =>
        {
            node.RoomType = (eRoomType)evt.newValue;
            node.title = node.RoomType.ToString(); // ���� ����ȭ
        });
        node.mainContainer.Add(typeField);

        AddElement(node);
        return node;
    }

    public RoomNode CreateRoomNode(RoomNode node, eRoomType roomType)
    {
        node.title = roomType.ToString(); // ��� ��ܿ� Ÿ�� ǥ��
        node.RoomType = roomType;
        node.SetPosition(new Rect(node.VirtualPos, new Vector2(200, 150)));

        // RoomType ��Ӵٿ� �߰�
        var typeField = new EnumField(node.RoomType);
        typeField.RegisterValueChangedCallback(evt =>
        {
            node.RoomType = (eRoomType)evt.newValue;
            node.title = node.RoomType.ToString(); // ���� ����ȭ
        });
        node.mainContainer.Add(typeField);

        AddElement(node);
        return node;
    }



    public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
    {
        var compatiblePorts = new List<Port>();

        ports.ForEach((port) =>
        {
            // �ڱ� �ڽ��� �����ϰ�,
            // �ݴ� �����̸鼭 Ÿ���� ���� ��Ʈ�� ���� ����
            if (startPort != port && startPort.direction != port.direction && startPort.portType == port.portType)
            {
                compatiblePorts.Add(port);
            }
        });

        return compatiblePorts;
    }

    public DungeonData SaveGraphData()
    {
        DungeonData dungeonData = ScriptableObject.CreateInstance<DungeonData>();

        // ��� ����
        foreach (var node in nodes)
        {
            if (node is RoomNode roomNode)
            {
                dungeonData.rooms.Add(new RoomData
                {
                    guid = roomNode.roomID,
                    position = roomNode.GetPosition().position,
                    roomType = roomNode.RoomType // RoomNode�� Ÿ�� �ʵ尡 �ִٰ� ����
                });
            }
        }

        // ���� ����
        foreach (var edge in edges)
        {
            if (edge.input.node is RoomNode toNode && edge.output.node is RoomNode fromNode)
            {
                string direction = edge.output.portName; // ex: "N", "E", etc.                
                dungeonData.links.Add(new RoomLinkData
                {
                    fromNodeGUID = fromNode.roomID,
                    toNodeGUID = toNode.roomID,
                    direction = direction
                });
            }
        }

        return dungeonData;
    }
}
#endif