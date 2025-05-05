
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField]
    private Player player;
    [SerializeField]
    private DungeonController dungeon;
    public DungeonMaker dungeonMaker;
    public static event Action OnGameOver;
    public static event Action OnNextFlow;
  
    //플레이어가 있는 현재 룸
    public Room curRoom;

    public Panel_GameOver gameOver;


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
        Application.targetFrameRate = 90;
    }

    private void Setup()
    {
        if (UserData.Instance == null)
        {
            var obj   = new GameObject("UserData");
            var userdata =  obj.AddComponent<UserData>();
            userdata.CurIndex = 0;
            userdata.CreateNewCharacter("nameless", userdata.CurIndex);
            
        }

        gameOver.SetData();

        StartCoroutine(CoGameStartSetup());
    }

    private IEnumerator CoGameStartSetup()
    {
        var loadplayerdata = UserData.Instance.LoadData();
        #region 플레이어 셋업
        player.attribute = loadplayerdata.playerAttribute;
        #endregion

        #region 던전 셋업
        dungeon = dungeonMaker.Build(loadplayerdata.dungeonLevel);
        dungeon.Setup();
        Room startRoom = dungeon.FindRoombyType(eRoomType.Start);        
        ChangeCurrentRoom(startRoom);
        #endregion
        yield return new WaitForEndOfFrame();

        if(SoundManager.instance != null)
        SoundManager.instance.PlayBGM(eBGMType.GameSoundTrack);
    }

    public void ChangeCurrentRoom(Room newCurrentRoom)
    {
        //올드 룸
        if(curRoom != null)
        {
            curRoom.DisableRoom();
        }

        if (newCurrentRoom != null)
        {
            newCurrentRoom.EnableRoom();
        }

        curRoom = newCurrentRoom;
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
    [HideInInspector]
    static public bool Leveling = false;
    private IEnumerator CoNextLevel()
    {
        yield return null;
        Leveling = true;
        OnNextFlow?.Invoke();
        var loadplayerdata = UserData.Instance.LoadData();
        loadplayerdata.dungeonLevel++;
        Destroy(dungeon.gameObject);
        dungeon = dungeonMaker.Build(loadplayerdata.dungeonLevel);
        dungeon.Setup();
        Room startRoom = dungeon.FindRoombyType(eRoomType.Start);
        ChangeCurrentRoom(startRoom);
        yield return new WaitForSeconds(1f);
        Leveling = false;

    }



    bool gameover = false;

    public void GameOver()
    {
        if (gameover == true)
            return;

        gameover = true;
        OnGameOver?.Invoke();
        SoundManager.instance.audioSource.Pause();
        SoundManager.instance.PlayEffect(eEffectType.Gameover,this.transform);
    }

    public void SetPlayerTarget(Monster monster)
    {
        if(player != null)
        {
            player.SetPlayerTarget(monster);
        }
    }

    public void ResetTarget()
    {
        player.ResetTarget();
    }
}