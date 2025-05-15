using UnityEngine;

public class GlobalDefine
{
    //플레이어
    public const float PlayerRotationBaseSpeed = 7.0f;
    public const float PlayerPortalDelayTime = 6.0f;

    public const float ScanBaseRadius = 5f;
    public const float FallDeath = -8f;
    public const string Trigger_JumpStart = "Trg_JumpStart";
    public static Color Red = Color.red;
    public static Collider[] Basebuffer = new Collider[4]; // 미리 할당된 버퍼
    public static string String_Item = "Item";
    public static string String_Monster = "Monster";    
    public static int FallingHash = Animator.StringToHash("Falling");
    public static int FallingEndHash = Animator.StringToHash("FallingEnd");
    public static int DeadHash = Animator.StringToHash("Die");
    public static WaitForSeconds FadeInDelayTime = new WaitForSeconds(0.8f);
    public static string String_Horizontal = "Horizontal";
    public static string String_Vertical = "Vertical";
    public static string String_Jump = "Jump";
    public static string String_Player = "Player";
    public static string String_Door = "Door";
    public static string String_Stairs = "Stairs";
}