using System.Collections.Generic;
using UnityEngine;

public class UserData : MonoBehaviour 
{
    public string userId;
    public List<GameData> slots;
    /// <summary>
    /// ���õ� ���� �÷��̾� �ε��� 0~3���� ���� -1�̸� ���õ�������
    /// </summary>
    public int CurIndex;


    public static UserData Instance;
    

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            CurIndex = -1;
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
        newSaveData.playerAttribute = new GameAttribute();
        GameObject obj = new GameObject();
        GameEffect defaultGE = obj.AddComponent<GameEffect>();
        defaultGE.modifierOp = eModifier.Add;
        defaultGE.effect = new GameAttribute();
        defaultGE.effect.MaxHart = 3;
        defaultGE.effect.CurHart = 3;
        defaultGE.effect.atk = 1;
        defaultGE.effect.attackSpeed = 1;
        defaultGE.effect.speed = 4;


        newSaveData.playerAttribute = defaultGE.ApplyGameplayEffectToSelf(newSaveData.playerAttribute);
        newSaveData.headIndex = 1;
        newSaveData.bodyIndex = 1;
        slots[index] = newSaveData;
    }

    public GameData LoadData()
    {
        return slots[CurIndex];
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
    public GameAttribute playerAttribute;
    public int bodyIndex;
    public int headIndex;
}

