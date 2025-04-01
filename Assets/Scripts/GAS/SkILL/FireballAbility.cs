using System;
using System.Threading.Tasks;
using UnityEngine;

public class FireballAbility : GameAbility
{
    protected override async Task ExecuteAbility()
    {
        Debug.Log(" 파이어볼 발사!");
        await Task.Delay(TimeSpan.FromSeconds(Duration)); // 지속 효과 처리
        Debug.Log(" 파이어볼 종료!");
    }
}
