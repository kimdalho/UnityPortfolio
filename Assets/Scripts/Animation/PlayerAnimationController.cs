using UnityEngine;

public class PlayerAnimationController :  AnimationControllerBase
{   
    public Player player;

    public void OnJumpStartAnimationComplete() // Animation event at end of JumpStart clip
    {
        SetState(AnimState.InAir);
    }

    public void OnLandAnimationComplete() // Animation event at end of Land clip
    {
        SetState(AnimState.Idle);
    }
}