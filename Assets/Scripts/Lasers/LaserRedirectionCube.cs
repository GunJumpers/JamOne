using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserRedirectionCube : MonoBehaviour
{
    public LaserEmitter emitter;
    public float disableTimer;
    public float maxDisableTimer = 0.5f;
    public float enableTimerRate;

    public void EnableLaser()
    {
        emitter.isEmitting = true;
        if(disableTimer < maxDisableTimer)
        {
            disableTimer += enableTimerRate * Time.deltaTime;
        }
        
        
    }

    public void DisableLaser()
    {
        emitter.isEmitting = false;
    }

    private void Update()
    {
        UpdateTimer();
    }

    void UpdateTimer()
    {

        if(disableTimer > 0)
        {
            disableTimer -= Time.deltaTime;
            emitter.targetAlpha = disableTimer / maxDisableTimer;
        }
        if(disableTimer <= 0 && emitter.isEmitting)
        {
            DisableLaser();
            emitter.targetAlpha = 0;
        }
    }

}
