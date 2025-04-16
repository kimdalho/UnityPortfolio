using UnityEngine;
[System.Serializable]
public class GameAttribute
{
    public int MaxHart;
    public int CurHart;
    public int atk;
    public int attackSpeed;
    public int speed;

    //모든 스탯의 최대 수치값
    public int MaxValue = 15;

    public GameAttribute()
    {

    }

    public GameAttribute(int MaxHart, int CurHart, int atk, int attackSpeed, int speed)
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
            a.attackSpeed + b.attackSpeed,
              a.speed * b.speed);
    }

}
