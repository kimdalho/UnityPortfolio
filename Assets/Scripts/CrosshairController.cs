using UnityEngine;
using UnityEngine.UIElements;

public class CrosshairController : MonoBehaviour
{
    private Player player;
    [SerializeField]
    private Transform crossHair;

    private void Start()
    {
        player = GameManager.instance.GetPlayer();
    }
    private void Update()
    {
        Action();
    }

    private void Action()
    {
       bool action = player.gameplayTagSystem.HasTag(eTagType.Player_State_HasAttackTarget);        
       crossHair.gameObject.SetActive(action);
    }

}
