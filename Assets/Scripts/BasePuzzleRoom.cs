using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BasePuzzleRoom : MonoBehaviour
{

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

    public void CompleteRoom()
    {
        if (GameManager.Instance.puzzleRooms.ContainsKey(this))
        {
            GameManager.Instance.puzzleRooms[this] = true;
        }
    }
}
