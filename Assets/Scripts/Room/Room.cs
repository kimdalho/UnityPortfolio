
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public Transform camPoint;

    public List<GameObject> doorlist;
    private int x;
    private int y;

    public List<Room> neighbor;

    public Room()
    {
        neighbor = new List<Room>();
        neighbor.Add(null);
        neighbor.Add(null);
        neighbor.Add(null);
        neighbor.Add(null);        
    }

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
                    doorlist[i].GetComponent<Door>().callback = () => SetCurrentRoom(index);
                }
            }
            else
            {
                SetDoor(i, false);
            }
        }


    }


    private void OnDestroy()
    {
        for (int i = 0; i < 4; i++)
        {
            doorlist[i].GetComponent<Door>().callback = null;
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
    public void SetCurrentRoom(int target)
    {
        if (target >= 4)
        {
            Debug.Log($"{target}");
            return;

        }
            
        GameManager GM = GameManager.instance;
        GM.SetCurrentRoom(this);
        GM.mainCam.transform.position = neighbor[target].camPoint.transform.position;
    }


}
