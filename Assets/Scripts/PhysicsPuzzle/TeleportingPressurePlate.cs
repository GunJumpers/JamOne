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
        isPressed = true;
        pressedEvent.Invoke();
        enableSound.Post(gameObject);
        foreach (Collider c in triggerList)
        {
            c.transform.parent.position = teleportLocation.position;
        }
    }

    public override void OnUnpressed()
    {
        isPressed = false;
        unpressedEvent.Invoke();

        foreach (Collider c in triggerList)
        {
            c.transform.parent.position = teleportLocation.position;
        }
    }
}
