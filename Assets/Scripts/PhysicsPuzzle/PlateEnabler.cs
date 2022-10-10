using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateEnabler : MonoBehaviour
{
    public PressurePlate plateToEnable;

    public void EnablePlate()
    {
        if (!plateToEnable.plateEnabled)
        {
            plateToEnable.plateEnabled = true;
        }
    }

    public void DisablePlate()
    {
        if (plateToEnable.plateEnabled)
        {
            plateToEnable.plateEnabled = false;
        }
    }
    
}
