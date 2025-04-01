using System;
using UnityEngine;

public class Item : MonoBehaviour
{
    private readonly String STR_PLAYER = "Player";

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == STR_PLAYER)
        {
            gameObject.SetActive(false);
        }
    }
}
