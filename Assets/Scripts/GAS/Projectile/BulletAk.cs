using System;
using UnityEngine;

public class BulletAk : Projectile
{
    protected override void Update()
    {
    }

    private void OnDestroy()
    {
        Debug.LogWarning($"[Bullet Destroyed] {gameObject.name} at {Time.time}");
        Debug.LogWarning(Environment.StackTrace);
    }
}