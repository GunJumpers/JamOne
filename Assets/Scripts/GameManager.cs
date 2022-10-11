using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : UnitySingleton<GameManager>
{

    public Dictionary<BasePuzzleRoom, bool> puzzleRooms;

    public override void Awake()
    {
        base.Awake();
        puzzleRooms = new Dictionary<BasePuzzleRoom, bool>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        
    }
}
