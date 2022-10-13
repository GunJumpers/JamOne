using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PhysicsPuzzleManager : BasePuzzleRoom
{
    public List<PressurePlate> requiredPlates;
    public UnityEvent onCompleteEvent;

    //public AK.Wwise.Event completionEventSound;

    [Header("Win Goal Objects")]
    public GameObject winGoalPrefab;
    public Transform winGoalSpawnPosition;

    public override void Start()
    {
        base.Start();


    }

    public void CheckPuzzleComplete()
    {
        if (isComplete)
        {
            return;
        }

        isComplete = true;
        foreach(PressurePlate p in requiredPlates)
        {
            if (!p.isPressed)
            {
                isComplete = false;
            }
        }

        if (isComplete)
        {
            //onCompleteEvent.Invoke();
            //completionEventSound.Post(gameObject);

            var goal = Instantiate(winGoalPrefab, winGoalSpawnPosition.position, Quaternion.identity);
            goal.GetComponent<GoalCompletion>().roomType = GameManager.RoomType.Cubes;

            CompleteRoom();
        }
    }

    




}
