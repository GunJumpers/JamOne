using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PhysicsPuzzleManager : BasePuzzleRoom
{
    public List<PressurePlate> requiredPlates;
    public UnityEvent onCompleteEvent;
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
            onCompleteEvent.Invoke();
            CompleteRoom();
        }
    }

    




}
