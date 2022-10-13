using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorPlate : MonoBehaviour
{
    public float glowTime;
    public Material defaultMaterial;
    public Material glowMaterial;
    public Animator anim;
    public Renderer plateRenderer;
    public AK.Wwise.Event noteSound = null;


    public void MakeGlow()
    {
        StartCoroutine(GlowRoutine());
    }

    public IEnumerator GlowRoutine()
    {
        plateRenderer.material = glowMaterial;

        yield return new WaitForSeconds(glowTime);

        plateRenderer.material = defaultMaterial;

    }

    public void ActivatePlate()
    {
        anim.Play("enable");
        MakeGlow();
        noteSound.Post(this.gameObject);
    }

    public void DeactivatePlate()
    {
        anim.Play("disable");
    }
}
