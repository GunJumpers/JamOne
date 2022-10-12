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
            plateToEnable.EnablePlate();
        }
    }

    public void DisablePlate()
    {
        if (plateToEnable.plateEnabled)
        {
            plateToEnable.DisablePlate();
        }
    }
    
}
