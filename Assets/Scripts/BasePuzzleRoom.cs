using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BasePuzzleRoom : MonoBehaviour
{
    public bool isComplete = false;
    // Start is called before the first frame update
    public virtual void Start()
    {
        InitializeRoomStatus();
    }

    public void InitializeRoomStatus()
    {
        if(GameManager.Instance != null)
        {
            GameManager.Instance.puzzleRooms.Add(this,false);
        }
    }

    public virtual void CompleteRoom()
    {
        if (GameManager.Instance.puzzleRooms.ContainsKey(this))
        {
            GameManager.Instance.puzzleRooms[this] = true;
        }
    }
}
