using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonController : Interactable
{
    public UnityEvent buttonEvent;
    public UnityEvent buttonOffCooldownEvent;
    public Animator anim;
    public bool onCooldown;
    public float buttonCooldown;

    public AK.Wwise.Event buttonPressSound;

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

        buttonPressSound.Post(gameObject);

        StartCoroutine(ButtonCooldownRoutine());

    }

    IEnumerator ButtonCooldownRoutine()
    {
        yield return new WaitForSeconds(buttonCooldown);
        buttonOffCooldownEvent.Invoke();
        onCooldown = false;
    }

}
