using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserRedirectionCube : MonoBehaviour
{
    public LaserEmitter emitter;
    public Renderer glowRenderer;
    [ColorUsageAttribute(true, true)]
    public Color minGlow;
    [ColorUsageAttribute(true, true)]
    public Color maxGlow;
    private Material mat;
    public float disableTimer;
    public float maxDisableTimer = 0.5f;
    public float enableTimerRate;

    private void Start()
    {
        mat = glowRenderer.material;
        mat.SetColor("_EmissionColor", minGlow);
    }

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
            mat.SetColor("_EmissionColor", Color.Lerp(minGlow, maxGlow, disableTimer / maxDisableTimer));
        }
        if(disableTimer <= 0 && emitter.isEmitting)
        {
            DisableLaser();
            emitter.targetAlpha = 0;
        }
    }

}
