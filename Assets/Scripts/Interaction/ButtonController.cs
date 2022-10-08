using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonController : Interactable
{
    public UnityEvent buttonEvent;
    public Animator anim;
    public bool onCooldown;
    public float buttonCooldown;

    public override void InteractAction()
    {
        if (onCooldown)
        {
            return;
        }

        base.InteractAction();
        onCooldown = true;
        buttonEvent.Invoke();
        if(anim != null)
        {
            anim.Play("press");
        }
        
        StartCoroutine(ButtonCooldownRoutine());

    }

    IEnumerator ButtonCooldownRoutine()
    {
        yield return new WaitForSeconds(buttonCooldown);
        onCooldown = false;
    }

}
