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
    
    [SerializeField]
    private Room ownerRoom;
    private int myIndex;

    IinputController inputController;
    /// <summary>
    /// 살짝 잘못만든구조 계단 내려가는 문을 초기화하는 함수 이건 인덱스가 없어서 별개로 만듬
    /// </summary>
    /// <param name="room"></param>
    public void SetFloorData(Room room)
    {
        this.ownerRoom = room;

    }
    
    /// <summary>
    /// 내 인덱스를 초기화하는 상위 스택에서 매개변수로 받아온다.
    /// </summary>
    /// <param name="room"></param>
    /// <param name="index"></param>
    public void SetDoorData(Room room,int index)
    {
        this.ownerRoom = room;
        myIndex = index;
    }


    // 도어 공식 메모
    // 0 = 2
    // 1 = 3
    // 2 = 0
    // 3 = 1

    public void InputyPress()
    {
       
        if (ownerRoom.state == eRoomType.Floor)
        {
            OpenNextFloor();
        }
        else
        {
            OpenRoomNeighbor();
        }
    }



    public void OpenRoomNeighbor()
    {
        GameManager GM = GameManager.instance;
        //활성화 룸 선택  
        int otherSide = GetOtherSide(myIndex); 
        Room nextRoom = ownerRoom.neighbor[myIndex];
        GM.SetCurrentRoom(nextRoom);
        Player player = GM.GetPlayer();
        player.SetPos(nextRoom.doorlist[otherSide].transform.position);

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


