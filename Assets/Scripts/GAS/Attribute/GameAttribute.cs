using UnityEngine;
[System.Serializable]
public class GameAttribute
{
    public float MaxHart;
    public float CurHart;
    public float atk;
    public float attackSpeed;
    public float speed;

    //모든 스탯의 최대 수치값
    private static int MaxValue = 30;

    public GameAttribute()
    {
        MaxHart = 0;
        CurHart = 0;
        atk = 0;
        attackSpeed = 0;
        speed = 0;
    }

    public GameAttribute(float MaxHart, float CurHart, float atk, float attackSpeed, float speed)
    {
        this.MaxHart = Mathf.Clamp(MaxHart,0, MaxValue);
        this.CurHart = Mathf.Clamp(CurHart, 0, MaxHart);
        this.atk = Mathf.Clamp(atk, 0, MaxValue);
        this.attackSpeed = Mathf.Clamp(attackSpeed, 0, MaxValue);
        this.speed = Mathf.Clamp(speed, 0, MaxValue);
    }

    public static GameAttribute operator +(GameAttribute a, GameAttribute b)
    {
        return new GameAttribute(
            a.MaxHart + b.MaxHart,
            a.CurHart + b.CurHart,
                a.atk + b.atk,
        a.attackSpeed + b.attackSpeed,                               
              a.speed + b.speed);
    }
    public static GameAttribute operator *(GameAttribute a, GameAttribute b)
    {
        return new GameAttribute(
            a.MaxHart * b.MaxHart,
            a.CurHart * b.CurHart,
                a.atk * b.atk,
            a.attackSpeed * b.attackSpeed,
              a.speed * b.speed);
    }

    public static GameAttribute operator /(GameAttribute a, GameAttribute b)
    {
        return new GameAttribute(
            a.MaxHart / b.MaxHart,
            a.CurHart / b.CurHart,
                a.atk / b.atk,
            a.attackSpeed / b.attackSpeed,
              a.speed / b.speed);
    }

    public static GameAttribute operator -(GameAttribute a, GameAttribute b)
    {
        return new GameAttribute(
            a.MaxHart - b.MaxHart,
            a.CurHart - b.CurHart,
                a.atk - b.atk,
            a.attackSpeed - b.attackSpeed,
              a.speed - b.speed);
    }

}