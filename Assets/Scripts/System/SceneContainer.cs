using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum eSceneType
{
    None = 0,  
    LobbyScene = 1,
    GameScene = 2,
    LoadingScene = 3,    
}
public class SceneContainer : MonoBehaviour
{
    public static SceneContainer Instance;

    private string targetType;
    private Dictionary<eSceneType,string> m_dic = new Dictionary<eSceneType,string>();

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SetData();
        }
        else
        {
            Destroy(this.gameObject);
        }

    
    }

    private void SetData()
    {
        m_dic.Add(eSceneType.LobbyScene, "LobbyScene");
        m_dic.Add(eSceneType.GameScene, "GameScene");
        m_dic.Add(eSceneType.LoadingScene, "LoadingScene");
    }


    public void LoadScene(eSceneType sceneType)
    {
        if (Instance == null || sceneType == eSceneType.None)
            return;

        targetType = m_dic[sceneType];
        SceneManager.LoadScene(m_dic[eSceneType.LoadingScene]);
    }

    public string GetTargetSceneString()
    {
        return targetType;
    }



}



