using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public struct StrRoom
{
    
    public Vector3 pos;
    public int x, y, z;
}

public class Roomgenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject roomPrefab;


    public float StartPosX;
    public float StartPosY;
    public float StartPosZ;

    public float PosX;
    public float PosY;
    public float PosZ;

    public int MAX_X;
    public int MIN_X;

    public int MAX_Y;
    public int MIN_Y;


    public int RoomCount;

    public Dictionary<int,Dictionary<int,Room>> grid = new Dictionary<int, Dictionary<int, Room>>();


    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.H))
        {
            BuildMap();
            SetNeighbor();
           
        }

    }
    /// <summary>
    /// 맵을 생성한다.
    /// </summary>
    private void BuildMap()
    {
        Room LastRoom = null;
        StrRoom tempRoom = new StrRoom();
        //처음 스타트룸 생성
        GameObject startRoom = Instantiate(roomPrefab);       
        LastRoom = startRoom.GetComponent<Room>();
        LastRoom.x = 0;
        LastRoom.y = 0;
        startRoom.gameObject.name = "StartRoom";
        startRoom.transform.position = new Vector3(StartPosX, StartPosY, StartPosZ);
        SetValue(0, 0, LastRoom);
        int roomCount = RoomCount;
 
        while (roomCount > 0)
        {
            tempRoom.x = LastRoom.x;
            tempRoom.y = LastRoom.y;
            var type = Random.Range(0, 4);
            switch(type)
            {
                case 0:
                    tempRoom.y = LastRoom.y + 1;
                    tempRoom.pos = new Vector3(LastRoom.transform.position.x, LastRoom.transform.position.y, LastRoom.transform.position.z + PosZ);
                    break;
                case 1:
                    tempRoom.x = LastRoom.x + 1;
                    tempRoom.pos = new Vector3(LastRoom.transform.position.x + PosX, LastRoom.transform.position.y, LastRoom.transform.position.z);
                    break;
                case 2:
                    tempRoom.y = LastRoom.y - 1;
                    tempRoom.pos = new Vector3(LastRoom.transform.position.x, LastRoom.transform.position.y, LastRoom.transform.position.z - PosZ);
                    break;
                case 3:
                    tempRoom.x = LastRoom.x - 1;
                    tempRoom.pos = new Vector3(LastRoom.transform.position.x - PosX, LastRoom.transform.position.y, LastRoom.transform.position.z);
                    break;
            }

            if (GetValue(tempRoom.x, tempRoom.y) == null)
            {
                Room room = Instantiate(roomPrefab).GetComponent<Room>();
                
                room.transform.position = new Vector3(tempRoom.pos.x, tempRoom.pos.y, tempRoom.pos.z);
                room.x = tempRoom.x;
                room.y = tempRoom.y;
                room.gameObject.name = $"room {room.x},{room.y}";
                SetValue(room.x, room.y, room);
                LastRoom = room;
                Debug.Log($"{LastRoom.x} {LastRoom.y}");
                roomCount--;
            }
        }


    }

  
    void SetValue(int x, int y, Room value)
    {
        if (!grid.ContainsKey(x))
        {
            grid[x] = new Dictionary<int, Room>();
        }
        grid[x][y] = value;
    }

    Room GetValue(int x, int y)
    {
        if (grid.ContainsKey(x) && grid[x].ContainsKey(y))
        {
            return grid[x][y];
        }
        return null;
    }
    /// <summary>
    /// 룸의 서로 이웃하는 맵의 정보를 각 룸들이 알고있다. 
    /// 이웃하는 룸의 정보로 맵의 문 상태를 업데이트 한다.
    /// </summary>
    private void SetNeighbor()
    {
        foreach (var row in grid) // row.Key는 X 좌표, row.Value는 Dictionary<int, T>
        {
            foreach (KeyValuePair<int,Room> col in row.Value) // col.Key는 Y 좌표, col.Value는 실제 값
            {
                Room room = col.Value;

                Room up = GetValue(room.x, room.y + 1);
                Room right = GetValue(room.x +1, room.y);
                Room down = GetValue(room.x, room.y - 1);
                Room left = GetValue(room.x -1, room.y);

                if(up != null)
                {
                    room.neighbor[0] = up;
                    room.SetDoor(0, true);
                }

                if (right != null)
                {
                    room.neighbor[1] = right;
                    room.SetDoor(1, true);
                }

                if (down != null)
                {
                    room.neighbor[2] = down;
                    room.SetDoor(2, true);
                }

                if (left != null)
                {
                    room.neighbor[3] = left;
                    room.SetDoor(3, true);
                }



            }
        }
    }

}

