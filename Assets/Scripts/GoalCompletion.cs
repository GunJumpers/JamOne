using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalCompletion : MonoBehaviour
{
    public GameManager.RoomType roomType;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            GameManager.Instance.CompletePuzzleRoom(roomType);
            Destroy(gameObject);
        }
    }
}
