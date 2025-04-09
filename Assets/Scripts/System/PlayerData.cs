using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour 
{
    public SaveData saveData;

    public static PlayerData Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}

[System.Serializable]
public class SaveData
{
    public string nickname;
    public GameAttribute attribute;
    public ePlayerType playerType;
}