using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LaserPuzzleManager : BasePuzzleRoom
{
    [SerializeField] private List<LaserReciever> _laserRecievers;
    public UnityEvent completionEvent;
    private bool puzzleCompleted;

    public override void Start()
    {
        base.Start();
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
        completionEvent.Invoke();
        puzzleCompleted = true;
        Debug.Log("Puzzle Complete!");
    }

}
