using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LaserPuzzleManager : BasePuzzleRoom
{
    [SerializeField] private List<LaserReciever> _laserRecievers;
    public UnityEvent completionEvent;
    private bool puzzleCompleted;

    [Header("Win Goal Objects")]
    public GameObject winGoalPrefab;
    public Transform winGoalSpawnPosition;

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

        var goal = Instantiate(winGoalPrefab, winGoalSpawnPosition.position, Quaternion.identity);
        goal.GetComponent<GoalCompletion>().roomType = GameManager.RoomType.Lasers;
    }

}
