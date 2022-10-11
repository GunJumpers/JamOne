using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportingPressurePlate : PressurePlate
{

    public Transform teleportLocation;

    public override void OnPressed()
    {
        if (!plateEnabled)
        {
            return;
        }
        base.OnPressed();
        foreach (Collider c in triggerList)
        {
            c.transform.parent.position = teleportLocation.position;
        }
    }
}
