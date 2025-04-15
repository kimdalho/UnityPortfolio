using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField]
    private Player player;
    public List<GameObject> roomlist;

    int index = 0;

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


    private void Start()
    {
        //마우스 커서 안보이게, 마우스 커서 고정
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        roomlist[index].gameObject.SetActive(true);
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
        roomlist[index].gameObject.SetActive(false);
        index++;
        roomlist[index].gameObject.SetActive(true);
        player.transform.position = Vector3.zero;
    }

}
