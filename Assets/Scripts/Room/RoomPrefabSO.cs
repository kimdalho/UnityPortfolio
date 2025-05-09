using UnityEngine;


[CreateAssetMenu(fileName = "Room Data Table", menuName = "Room/Room Data")]

public class RoomPrefabSO : ScriptableObject
{
    public eRoomType roomType;
    public GameObject prefab;
}