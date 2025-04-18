using System.Collections.Generic;
using UnityEngine;

public class UserData : MonoBehaviour 
{
    public List<GameData> slots;

    public int CurIndex;


    public static UserData Instance;
    

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
    /// 새로운 캐릭터를 생성되면 고정된 GE와 1계층으로 생성된다.
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
        slots[index] =  newSaveData;
    }

    public GameData LoadData()
    {
        return slots[CurIndex];
    }

}

/// <summary>
/// 플레이어 데이터는 최대 3개까지 생성가능하고 저장한다. 저장된 데이터는 index를 통해 관리된다.
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

