using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RoomDetector : MonoBehaviour
{

    public bool containsPlayer;
    public UnityEvent enterEvent;
    public UnityEvent exitEvent;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            containsPlayer = true;
            enterEvent.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            containsPlayer = false;
            exitEvent.Invoke();
        }
    }
}
