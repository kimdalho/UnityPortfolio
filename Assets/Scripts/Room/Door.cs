using System;
using UnityEngine;
using UnityEngine.Windows;

/// <summary>
/// ��
/// �÷��̾ �浹�� ���� �� ���¿��� ��ȣ�ۿ� �� �ش� �̿��Ǵ� ���� ������ �÷��̾ �̵���Ų��.
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
    /// ��¦ �߸����籸�� ��� �������� ���� �ʱ�ȭ�ϴ� �Լ� �̰� �ε����� ��� ������ ����
    /// </summary>
    /// <param name="room"></param>
    public void SetFloorData(Room room)
    {
        this.ownerRoom = room;

    }
    
    /// <summary>
    /// �� �ε����� �ʱ�ȭ�ϴ� ���� ���ÿ��� �Ű������� �޾ƿ´�.
    /// </summary>
    /// <param name="room"></param>
    /// <param name="index"></param>
    public void SetDoorData(Room room,int index)
    {
        this.ownerRoom = room;
        myIndex = index;
    }


    // ���� ���� �޸�
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
        //Ȱ��ȭ �� ����  
        int otherSide = GetOtherSide(myIndex); 
        Room nextRoom = ownerRoom.neighbor[myIndex];
        GM.SetCurrentRoom(nextRoom);
        Player player = GM.GetPlayer();
        player.SetPos(nextRoom.doorlist[otherSide].transform.position);

    }

    /// <summary>
    /// �������� �ִ��� Ž�� ���� ������ ���� ����
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
                    Debug.LogError("���� �߻�");
                    return 0;
                }
                
        }
    }
}


