
using System.Collections;
using UnityEngine;

/// <summary>
/// ��
/// �÷��̾ �浹�� ���� �� ���¿��� ��ȣ�ۿ� �� �ش� �̿��Ǵ� ���� ������ �÷��̾ �̵���Ų��.
/// 
/// </summary>
/// 




public class Portal : MonoBehaviour
{
    public Room toNextRoom;
    public string fromNodeGUID;
    public string toNodeGUID;
    public JumpPoint toNextSpawnPoint;
    public JumpPoint SpawnPoint;
    public PlayerTraversal traversal;


    public void OnTriggerEnter(Collider other)
    {
        var tagSystem = GameManager.instance.GetPlayer().gameplayTagSystem;
        if (tagSystem.HasTag(eTagType.Player_State_IgnorePortal) == true)
            return;

        if (other.gameObject.tag == "Player" && 
            this.tag == "Door")
        {
            Player player = other.GetComponent<Player>();
            traversal.StartPlayerTraversal(player, this);
        }
        else if(other.gameObject.tag == "Player" 
            && this.tag == "Stairs")
        {            
            var GM = GameManager.instance;
            GM.GoToNextFloor();
        }
    }
}
