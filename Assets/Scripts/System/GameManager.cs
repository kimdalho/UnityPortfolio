using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField]
    private Player player;
    private GameObject dungeon;
    public DungeonMaker dungeonMaker;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
           Destroy(this.gameObject);
        }
    }

    private void Setup()
    {
        #region ���콺 �¾�
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        #endregion

        if (UserData.Instance == null)
        {
            var obj   = new GameObject("UserData");
            var userdata =  obj.AddComponent<UserData>();
            userdata.CurIndex = 0;
            userdata.CreateNewCharacter("nameless", userdata.CurIndex);

        }

        var loadplayerdata = UserData.Instance.LoadData();
        #region �÷��̾� �¾�
        player.attribute = loadplayerdata.playerAttribute;
        #endregion

        #region ���� �¾�
        dungeon = dungeonMaker.Build(loadplayerdata.dungeonLevel);      
        #endregion
    }



    private void Start()
    {
        Setup();

    }


    public Player GetPlayer()
    {
        return player; 
    }

    public void GoToNextFloor()
    {
        StartCoroutine(CoNextLevel());       
    }

    private IEnumerator CoNextLevel()
    {
        yield return null;
        var loadplayerdata = UserData.Instance.LoadData();
        loadplayerdata.dungeonLevel++;

        Destroy(dungeon);
        dungeon = dungeonMaker.Build(loadplayerdata.dungeonLevel);

        player.transform.position = Vector3.zero;
    }

}
