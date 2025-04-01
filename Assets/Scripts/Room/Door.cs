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

    // ���� ���� �޸�
    // 0 = 2
    // 1 = 3
    // 2 = 0
    // 3 = 1



    private void OpenRoomNeighbor()
    {
        GameManager GM = GameManager.instance;
        //Ȱ��ȭ �� ����
       
        GM.SetCurrentRoom(this.ownerRoom);
        Player player = GM.GetPlayer();

        int otherSide = GetOtherSide(myIndex);
        
        Room nextRoom = ownerRoom.neighbor[myIndex];

        player.transform.position = nextRoom.doorlist[otherSide].transform.position;
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


