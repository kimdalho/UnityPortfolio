using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Room currentRoom;
    public static GameManager instance;
    [SerializeField]
    Roomgenerator roomgenerator;
    [SerializeField]
    public GameObject playerPrefab;
    private Player player;

    public Camera mainCam;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
        }
    }

    private void Start()
    {
        roomgenerator.SetData();
        player = Instantiate(playerPrefab).gameObject.GetComponent<Player>();
        player.transform.position = roomgenerator.startroom.spawnPoint.transform.position;

    }

    public void SetCurrentRoom(Room room)
    {
        this.currentRoom = room;
    }

    public Player GetPlayer()
    {
        return player; 
    }

}
