
using System;
using UnityEngine;

public interface IOnKillEvent
{
   public void OnKill();
}


public class AttributeEntity : MonoBehaviour , IOnKillEvent
{
    public delegate void OnHitdelegate();
    
    public OnHitdelegate OnHit;

    public GameAttribute attribute;

    public Action Onkill;

    public void OnKill()
    {
        Onkill?.Invoke();
    }
}