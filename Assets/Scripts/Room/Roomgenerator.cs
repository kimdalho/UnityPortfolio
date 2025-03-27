using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct StrRoom
{
    public Vector3 pos;
    public int x, y, z;
}

public class Roomgenerator : MonoBehaviour
{
    public StartRoom startroom;

    [SerializeField]
    private GameObject roomPrefab;
    [SerializeField]
    private GameObject startRoomPrefab;


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


    public void SetData()
    {
        BuildMap();
        SetNeighbor();
    }

    /// <summary>
    /// 맵을 생성한다.
    /// </summary>
    private void BuildMap()
    {
        Room LastRoom = null;
        StrRoom str_tempRoom = new StrRoom();
        //처음 스타트룸 생성
        GameObject startObject = Instantiate(startRoomPrefab);       
        LastRoom = startObject.GetComponent<Room>();
        LastRoom.SetRoom("StartRoom",0, 0);
        GameManager.instance.SetCurrentRoom(LastRoom);
        startObject.transform.position = new Vector3(StartPosX, StartPosY, StartPosZ);
        startroom = startObject.GetComponent<StartRoom>();
        SetGridValue(0, 0, LastRoom);
        int roomCount = RoomCount;
 
        while (roomCount > 0)
        {


            str_tempRoom.x = LastRoom.GetX();
            str_tempRoom.y = LastRoom.GetY();
            var type = Random.Range(0, 4);
            switch(type)
            {
                case 0:
                    str_tempRoom.y = LastRoom.GetY() + 1;
                    str_tempRoom.pos = new Vector3(LastRoom.transform.position.x, LastRoom.transform.position.y, LastRoom.transform.position.z + PosZ);
                    break;
                case 1:
                    str_tempRoom.x = LastRoom.GetX() + 1;
                    str_tempRoom.pos = new Vector3(LastRoom.transform.position.x + PosX, LastRoom.transform.position.y, LastRoom.transform.position.z);
                    break;
                case 2:
                    str_tempRoom.y = LastRoom.GetY() - 1;
                    str_tempRoom.pos = new Vector3(LastRoom.transform.position.x, LastRoom.transform.position.y, LastRoom.transform.position.z - PosZ);
                    break;
                case 3:
                    str_tempRoom.x = LastRoom.GetX() - 1;
                    str_tempRoom.pos = new Vector3(LastRoom.transform.position.x - PosX, LastRoom.transform.position.y, LastRoom.transform.position.z);
                    break;
            }

            if (GetValue(str_tempRoom.x, str_tempRoom.y) == null)
            {
                Room room = Instantiate(roomPrefab).GetComponent<Room>();
                var position = new Vector3(str_tempRoom.pos.x, str_tempRoom.pos.y, str_tempRoom.pos.z);

                eRoomType seletType = roomCount == 1 ? eRoomType.Floor : eRoomType.Neighbor;
                
                room.CreateRoom(seletType, position, str_tempRoom);

                SetGridValue(room.GetX(), room.GetY(), room);
                LastRoom = room;
                roomCount--;
            }
        }
    }

  
    void SetGridValue(int x, int y, Room value)
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

                Room up = GetValue(room.GetX(), room.GetY() + 1);
                Room right = GetValue(room.GetX() +1, room.GetY());
                Room down = GetValue(room.GetX(), room.GetY() - 1);
                Room left = GetValue(room.GetX() -1, room.GetY());

                room.SetNeighbor(up, right, down, left);
            }
        }
    }

}

