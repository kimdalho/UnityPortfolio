
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;


public enum eRoomType
{
    Neighbor = 0,
    Floor = 1,
}



public class Room : MonoBehaviour
{
    public eRoomType state;
    public Transform camPoint;

    public List<GameObject> doorlist;
    private int x;
    private int y;

    public Door nextFloorDoor;
    public List<Room> neighbor;
    


    public void CreateRoom(eRoomType type, Vector3 position, StrRoom strPos)
    {
        state = type;
        neighbor = new List<Room>
        {
            null,
            null,
            null,
            null
        };

        transform.position = position;
        SetRoom(strPos.x, strPos.y);
        nextFloorDoor.SetFloorData(this);
        nextFloorDoor.gameObject.SetActive(type == eRoomType.Floor);
    }

    /// <summary>
    /// 이웃한 룸의 정보를 리스트로 가지고온다.
    /// </summary>
    /// <param name="up"></param>
    /// <param name="right"></param>
    /// <param name="down"></param>
    /// <param name="left"></param>
    public void SetNeighbor(Room up, Room right, Room down, Room left)
    {
        Room[] rooms = { up, right, down, left };

        for (int i = 0; i < 4; i++)
        {
            if (rooms[i] != null)
            {
                neighbor[i] = rooms[i];
                SetDoor(i, true);

                if (doorlist[i] != null)
                {                    
                    int index = i; // 람다 캡처 문제 방지
                    //람다 캡처 콜백을 담을시 index가 value 형태가 아니라 ref 형태로 4만을 반환하게 될수있다. 
                    Door door = doorlist[i].GetComponent<Door>();
                    if(door != null)
                    {
                        door.SetDoorData(this,index);
                    }
                }
            }
            else
            {
                SetDoor(i, false);
            }
        }
    }

    public void SetDoor(int dir, bool isOpen)
    {
        doorlist[dir].gameObject.SetActive(isOpen);
    }

    public void SetRoom(string roomName, int x, int y)
    {
        gameObject.name = roomName;
        this.x = x;
        this.y = y;
    }
    /// <summary>
    /// 이름을 좌표 기준으로 정의한다.
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    public void SetRoom(int x, int y)
    {
        this.x = x;
        this.y = y;
        gameObject.name = $"room {this.x},{this.y}";
    }

    public int GetX() { return x; }
    public int GetY() { return y; }



    /// <summary>
    /// 활성화될 룸을 선택 문 트리거 발생 시
    /// </summary>

}
