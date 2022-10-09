using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserPuzzleManager : UnitySingleton<LaserPuzzleManager>
{
    [SerializeField] private List<LaserReciever> _laserRecievers;
    private bool puzzleCompleted;

    public void Start()
    {
        puzzleCompleted = false;
    }
    public void CheckEnabled()
    {
        if (puzzleCompleted)
        {
            return; 
        }

        foreach (LaserReciever r in _laserRecievers)
        {
            if (!r.isEnabled)
            {
                return;
            }
        }

        CompletedPuzzle();
        
    }

    void CompletedPuzzle()
    {
        puzzleCompleted = true;
        Debug.Log("Puzzle Complete!");
    }

}
