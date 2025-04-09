using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Room currentRoom;
    public static GameManager instance;
    public AbilitySystem abilitySystem;

    [SerializeField]
    Roomgenerator roomgenerator;
    //[SerializeField]
    //public GameObject playerPrefab;
    [SerializeField]
    private Player player;
    [SerializeField]

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


        //���콺 Ŀ�� �Ⱥ��̰�, ���콺 Ŀ�� ����
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        roomgenerator.SetData();
        //player = Instantiate(playerPrefab).gameObject.GetComponent<Player>();
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
