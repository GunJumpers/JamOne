using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuidanceWire : MonoBehaviour
{
    public bool isEnabled;
    public Material mat;
    [ColorUsageAttribute(true, true)]
    public Color enableColor;
    [ColorUsageAttribute(true, true)]
    public Color disableColor;

    private void Start()
    {
        mat = GetComponent<Renderer>().material;
        if (isEnabled)
        {
            EnableWire();
        }
    }

    public void EnableWire()
    {
        mat.SetColor("_EmissionColor", enableColor);
        isEnabled = true;
    }

    public void DisableWire()
    {
        mat.SetColor("_EmissionColor", disableColor);
        isEnabled = false;
    }
}
