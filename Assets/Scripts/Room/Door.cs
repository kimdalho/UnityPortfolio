using System;
using UnityEngine;

public class Door : MonoBehaviour
{

    public Action callback;
    private void OnTriggerEnter(Collider other)
    {
      
        if (other.tag == "Player")
        {
           callback.Invoke();
           //Open()
        }

    }

    private void Open()
    {

    }
}
