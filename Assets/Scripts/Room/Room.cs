
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
    /// �̿��� ���� ������ ����Ʈ�� ������´�.
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
                    int index = i; // ���� ĸó ���� ����
                    //���� ĸó �ݹ��� ������ index�� value ���°� �ƴ϶� ref ���·� 4���� ��ȯ�ϰ� �ɼ��ִ�. 
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
    /// �̸��� ��ǥ �������� �����Ѵ�.
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
    /// Ȱ��ȭ�� ���� ���� �� Ʈ���� �߻� ��
    /// </summary>

}
