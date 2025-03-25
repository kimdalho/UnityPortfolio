
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public Transform camPoint;

    public List<GameObject> doorlist;
    public int x;
    public int y;

    public List<Room> neighbor;
    public List<int> neighbortest;

    public Room()
    {
        
        neighbor = new List<Room>();
        neighbor.Add(null);
        neighbor.Add(null);
        neighbor.Add(null);
        neighbor.Add(null);


        neighbortest = new List<int>();
        neighbortest.Add(10);

    }

    public void SetDoor(int dir, bool isOpen)
    {
        doorlist[dir].gameObject.SetActive(!isOpen);
    }
}
