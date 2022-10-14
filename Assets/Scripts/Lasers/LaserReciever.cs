using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LaserReciever : MonoBehaviour
{

    public float disableTimer;
    public float maxDisableTimer = 0.5f;
    public float enableTimerRate;
    public bool isEnabled;
    public UnityEvent enableEvent;
    public UnityEvent disableEvent;
    public AK.Wwise.Event activateSFX;

    public void EnableReciever()
    {
        if (!isEnabled)
        {
            isEnabled = true;
            activateSFX.Post(this.gameObject);
            enableEvent.Invoke();
        }
        
        if (disableTimer < maxDisableTimer)
        {
            disableTimer += enableTimerRate * Time.deltaTime;
        }
    }

    public void DisableReciever()
    {
        disableEvent.Invoke();
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
