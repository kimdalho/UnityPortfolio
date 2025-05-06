using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class UserData : MonoBehaviour 
{
    public List<GameData> slots;

    public int CurIndex;


    public static UserData Instance;

    public int level1_itemCount;
    public int level2_itemCount;
    public int level3_itemCount;

    public int level1_killMonster;
    public int level2_killMonster;
    public int level3_killMonster;




    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            slots = new List<GameData>();
            slots.AddRange(new GameData[4]);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    /// <summary>
    /// ���ο� ĳ���͸� �����Ǹ� ������ GE�� 1�������� �����ȴ�.
    /// </summary>
    /// <param name="_nickname"></param>
    /// <param name="index"></param>
    public void CreateNewCharacter(string _nickname,int index)
    {
        GameData newSaveData = new GameData();
        newSaveData.index = index;
        newSaveData.nickname = _nickname;
        newSaveData.dungeonLevel = 1;
        newSaveData.playerdata = new AttributeSet();

        newSaveData.playerdata.SetValue(eAttributeType.Health, 3,3);
        newSaveData.playerdata.SetValue(eAttributeType.Attack, 1);
        newSaveData.playerdata.SetValue(eAttributeType.AttackSpeed, 1);
        newSaveData.playerdata.SetValue(eAttributeType.Speed, 4);

        newSaveData.headIndex = 1;
        newSaveData.bodyIndex = 1;
        slots[index] =  newSaveData;
    }

    public GameData LoadData()
    {
        return slots[CurIndex];
    }

    public void SetKillMonster(int monsterlevel)
    {
        switch (monsterlevel)
        {
            case 1:
                level1_killMonster++;
                break;
            case 2:
                level2_killMonster++;
                break;
            case 3:
                level3_killMonster++;
                break;
        }
    }

    public void SetPickupedItem(int itemRank)
    {
        switch (itemRank)
        {
            case 1:
                level1_itemCount++;
                break;
            case 2:
                level2_itemCount++;
                break;
            case 3:
                level3_itemCount++;
                break;
        }
    }

    public void DataReset()
    {
        slots[CurIndex] = new GameData();
        CurIndex = -1;
        level1_itemCount = 0;
        level2_itemCount = 0;
        level3_itemCount = 0;

       level1_killMonster = 0;
       level2_killMonster = 0;
       level3_killMonster = 0;
    }
}

/// <summary>
/// �÷��̾� �����ʹ� �ִ� 3������ ���������ϰ� �����Ѵ�. ����� �����ʹ� index�� ���� �����ȴ�.
/// </summary>
[System.Serializable]
public class GameData
{    
    public int index;
    public string nickname;
    public int dungeonLevel;    
    public AttributeSet playerdata;
    public int bodyIndex;
    public int headIndex;
}

