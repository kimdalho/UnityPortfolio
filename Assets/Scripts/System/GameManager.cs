using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Room currentRoom;
    public static GameManager instance;
    [SerializeField]
    Roomgenerator roomgenerator;
    [SerializeField]
    public GameObject playerPrefab;

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
        Player player = Instantiate(playerPrefab).gameObject.GetComponent<Player>();
        player.transform.position = roomgenerator.startroom.spawnPoint.transform.position;

    }

    public void SetCurrentRoom(Room room)
    {
        this.currentRoom = room;

    }

}
