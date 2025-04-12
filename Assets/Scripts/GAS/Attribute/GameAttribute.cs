using UnityEngine;
[System.Serializable]
public class GameAttribute
{
    public int MaxHart;
    public int CurHart;
    public int atk;
    public int attackSpeed;
    public int speed;   


    public GameAttribute(int MaxHart, int CurHart, int atk, int attackSpeed, int speed)
    {
        this.MaxHart = MaxHart;
        this.CurHart = CurHart;
        this.atk = atk;
        this.attackSpeed = attackSpeed;
        this.speed = speed;
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
