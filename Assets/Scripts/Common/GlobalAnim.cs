using UnityEngine;

public enum AnimState
{
    Idle,
    Move,
    Attack,
    Reload,
    JumpStart,
    InAir,
    Land,
    Death
}
public class GlobalAnim
{  
   public static string MoveX = "MoveX";
   public static string MoveY = "MoveY";
   public static string IsMove = "IsMove";
   public static string IsJumping = "IsJumping";
   public static string IsFalling = "IsFalling";
   public static string IsLanding = "IsLanding";
   public static string IsAttacking = "IsAttacking";
   public static string IsReloading = "IsReloading";
   public static string IsDead = "IsDead";

}