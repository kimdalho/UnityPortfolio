using System;
using UnityEngine;
using UnityEngine.Windows;

/// <summary>
/// 문
/// 플레이어가 충돌을 유지 된 상태에서 상호작용 시 해당 이웃되는 룸의 문으로 플레이어를 이동시킨다.
/// 
/// </summary>
/// 


public class Door : MonoBehaviour
{
    private Room ownerRoom;
    private int myIndex;


    public void SetData(Room room)
    {
        this.ownerRoom = room;
    }
    
    public void SetData(Room room, int index)
    {
        this.ownerRoom = room;
        myIndex = index;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            if(UnityEngine.Input.GetKeyDown(KeyCode.F))
            {
                if(ownerRoom.state == eRoomType.Floor)
                {
                    OpenNextFloor();
                }
                else
                {
                    Debug.Log("check2");
                    OpenRoomNeighbor();
                }
               
            }
        }

    }

    // 도어 공식 메모
    // 0 = 2
    // 1 = 3
    // 2 = 0
    // 3 = 1



    private void OpenRoomNeighbor()
    {
        GameManager GM = GameManager.instance;
        //활성화 룸 선택
       
        GM.SetCurrentRoom(this.ownerRoom);
        Player player = GM.GetPlayer();

        int otherSide = GetOtherSide(myIndex);
        
        Room nextRoom = ownerRoom.neighbor[myIndex];

        player.transform.position = nextRoom.doorlist[otherSide].transform.position;
    }

    /// <summary>
    /// 다음층이 있는지 탐색 이후 없으면 게임 엔드
    /// </summary>
    private void OpenNextFloor()
    {
        Debug.Log("GameEnd");
    }


    public int GetOtherSide(int input)
    {
        switch (input)
        {
            case 0: return 2;
            case 1: return 3;
            case 2: return 0;
            case 3: return 1;
            default :
                {
                    Debug.LogError("버그 발생");
                    return 0;
                }
                
        }
    }
}


