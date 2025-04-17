#if UNITY_EDITOR

using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;



public class RoomNode : Node
{
    public string roomID;

    public eRoomType RoomType;
    public Vector2 VirtualPos;

    private Dictionary<string, Port> inputPorts = new Dictionary<string, Port>();
    private Dictionary<string, Port> outputPorts = new Dictionary<string, Port>();

    public RoomNode()
    {
        title = "Room";
        roomID = System.Guid.NewGuid().ToString();
        RoomType = eRoomType.Empty; // 기본값
        Initialize();
    }

    public RoomNode(Vector2 position)
    {
        roomID = System.Guid.NewGuid().ToString();
        RoomType = eRoomType.Empty; // 기본값
        VirtualPos = position;
        Initialize();
    }

    

    public void Initialize()
    {
        AddPort("Input Top", Direction.Input);
        AddPort("Input Bottom", Direction.Input);
        AddPort("Input Left", Direction.Input);
        AddPort("Input Right", Direction.Input);

        AddPort("Output Top", Direction.Output);
        AddPort("Output Bottom", Direction.Output);
        AddPort("Output Left", Direction.Output);
        AddPort("Output Right", Direction.Output);
    }

    private void AddPort(string portName, Direction direction)
    {
        var port = Port.Create<Edge>(Orientation.Horizontal, direction, Port.Capacity.Single, typeof(float));
        port.portName = portName;

        if (direction == Direction.Input)
        {
            inputPorts[portName] = port;
            inputContainer.Add(port);
        }
        else
        {
            outputPorts[portName] = port;
            outputContainer.Add(port);
        }
    }

    public Port GetInputPort(string portName)
    {
        if (inputPorts.TryGetValue(portName, out var port))
            return port;
        Debug.LogWarning($"Input port '{portName}' not found in node {this.name}");
        return null;
    }

    public Port GetOutputPort(string portName)
    {
        if (outputPorts.TryGetValue(portName, out var port))
            return port;
        Debug.LogWarning($"Output port '{portName}' not found in node {this.name}");
        return null;
    }

}
#endif