
using System;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField]
    private Player player;
    [SerializeField]
    private DungeonController dungeon;
    public DungeonMaker dungeonMaker;

    public static event Action OnNextFlow;

    //�÷��̾ �ִ� ���� ��
    public Room curRoom;

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

        StartCoroutine(CoGameStartSetup());
    }

    private IEnumerator CoGameStartSetup()
    {
        var loadplayerdata = UserData.Instance.LoadData();
        #region �÷��̾� �¾�
        player.attribute = loadplayerdata.playerAttribute;
        #endregion

        #region ���� �¾�
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
        //�õ� ��
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
        Destroy(dungeon);
        dungeon = dungeonMaker.Build(loadplayerdata.dungeonLevel);
        dungeon.Setup();
        Room startRoom = dungeon.FindRoombyType(eRoomType.Start);
        ChangeCurrentRoom(startRoom);
        yield return new WaitForSeconds(1f);
        Leveling = false;

    }
   

    public static event Action OnGameOver;

    public void GameOver()
    {
        Debug.Log("TODO: GameOver");
        OnGameOver?.Invoke();
    }

}
