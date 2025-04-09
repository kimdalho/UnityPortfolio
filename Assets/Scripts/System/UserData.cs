using System.Collections.Generic;
using UnityEngine;

public class UserData : MonoBehaviour 
{
    public SaveData saveData;

    public static UserData Instance;

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
    public int bodyIndex;
    public int headIndex;
    public int weaponIndex;
}