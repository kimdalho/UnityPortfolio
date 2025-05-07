
using System.Collections;
using UnityEngine;

/// <summary>
/// 문
/// 플레이어가 충돌을 유지 된 상태에서 상호작용 시 해당 이웃되는 룸의 문으로 플레이어를 이동시킨다.
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
        var tagSystem = other.GetComponent<IGameAbilityCharacterService>().GetGameplayTagSystem();
        if (tagSystem.HasTag(eTagType.Player_State_IgnorePortal) == true)
            return;

        if (other.gameObject.tag == GlobalDefine.String_Player && 
            this.tag == GlobalDefine.String_Door)
        {
            Player player = other.GetComponent<Player>();
            traversal.StartPlayerTraversal(player, this);
        }
        else if(other.gameObject.tag == GlobalDefine.String_Player
            && this.tag == GlobalDefine.String_Stairs)
        {            
            var GM = GameManager.instance;
            GM.GoToNextFloor();
        }
    }
}
