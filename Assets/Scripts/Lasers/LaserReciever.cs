using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserReciever : MonoBehaviour
{

    public float disableTimer;
    public float maxDisableTimer = 0.5f;
    public float enableTimerRate;
    public bool isEnabled;

    public void EnableReciever()
    {
        if (!isEnabled)
        {
            isEnabled = true;
            LaserPuzzleManager.Instance.CheckEnabled();
        }
        
        if (disableTimer < maxDisableTimer)
        {
            disableTimer += enableTimerRate * Time.deltaTime;
        }
    }

    public void DisableReciever()
    {
        LaserPuzzleManager.Instance.CheckEnabled();
        isEnabled = false;
    }

    private void FixedUpdate()
    {
        UpdateTimer();
    }

    void UpdateTimer()
    {

        if (disableTimer > 0)
        {
            disableTimer -= Time.deltaTime;
        }
        if (disableTimer <= 0 && isEnabled)
        {
            DisableReciever();
        }
    }
}
