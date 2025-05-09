
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
    public DungeonGenerator dungeonMaker;
    public static event Action OnGameOver;
    public static event Action OnNextFlow;
  
    //플레이어가 있는 현재 룸
    public Room curRoom;

    public Panel_GameOver gameOver;

    [SerializeField]
    private BossHud bossHud;


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
        player.attribute = loadplayerdata.playerdata;
        #endregion

        #region 던전 셋업
        dungeon = dungeonMaker.Build(loadplayerdata.dungeonLevel);
        Room startRoom = dungeon.FindRoombyType(eRoomType.Start);        
        yield return ChangeCurrentRoom(startRoom);
        #endregion

        if(SoundManager.instance != null)
        SoundManager.instance.PlayBGM(eBGMType.GameSoundTrack);
    }

    public IEnumerator ChangeCurrentRoom(Room newCurrentRoom)
    {
        //올드 룸
        if(curRoom != null)
        {
            curRoom.DisableRoom();
        }

      
        if (newCurrentRoom != null)
        {
            curRoom = newCurrentRoom;
            dungeon.EnterTheRoom(curRoom);
            curRoom.OpenToRoomPortals();
        }

        yield return StartCoroutine(CoCheckBoss());

    }

    private IEnumerator CoCheckBoss()
    {        
        switch (curRoom.roomType)
        {
            case eRoomType.Boss:
                Monster bossMonster = curRoom.roomMonsters[0];
                yield return bossHud.SetData(bossMonster);
                bossMonster.onlyIdle = false;
                bossMonster.OnDeath += BossDead;
                break;
            case eRoomType.Monster:
                foreach (Monster monster in curRoom.roomMonsters)
                {
                    monster.onlyIdle = false;
                }
                break;
        }
    }

    public void BossDead(Monster boss)
    {
        SoundManager Sm = SoundManager.instance;
        if (Sm != null)
        {
            Sm.PlayBGM(eBGMType.GameSoundTrack);          
        }
        bossHud.Hide();
        curRoom.Next.gameObject.SetActive(true);
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
    static public bool isAnimAction = false;
    private IEnumerator CoNextLevel()
    {
        yield return null;
        isAnimAction = true;
        OnNextFlow?.Invoke();
        var loadplayerdata = UserData.Instance.LoadData();
        loadplayerdata.dungeonLevel++;
        Destroy(dungeon.gameObject);
        dungeon = dungeonMaker.Build(loadplayerdata.dungeonLevel);
        Room startRoom = dungeon.FindRoombyType(eRoomType.Start);
        yield return ChangeCurrentRoom(startRoom);        
        isAnimAction = false;

    }

    bool gameover = false;

    public void GameOver()
    {
        if (gameover == true)
            return;

        gameover = true;
        dungeon.OnGameover();
        OnGameOver?.Invoke();
        bossHud.Hide();
        SoundManager.instance.audioSource.Pause();
        SoundManager.instance.PlayEffect(eEffectType.Gameover,this.transform);
    }
}