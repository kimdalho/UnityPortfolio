
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

    //플레이어가 있는 현재 룸
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

    private IEnumerator CoNextLevel()
    {
        yield return null;
        var loadplayerdata = UserData.Instance.LoadData();
        loadplayerdata.dungeonLevel++;

        Destroy(dungeon);
        dungeon = dungeonMaker.Build(loadplayerdata.dungeonLevel);
        dungeon.Setup();
        Room startRoom = dungeon.FindRoombyType(eRoomType.Start);
        ChangeCurrentRoom(startRoom);

        player.transform.position = Vector3.zero;
    }

    public void GameOver()
    {
        Debug.Log("TODO: GameOver");
    }

}
