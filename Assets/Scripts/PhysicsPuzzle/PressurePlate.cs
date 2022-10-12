using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PressurePlate : MonoBehaviour
{
    public bool isPressed;
    public bool plateEnabled;
    public Animator anim;
    protected List<Collider> triggerList;
    public UnityEvent pressedEvent;
    public UnityEvent unpressedEvent;

    public AK.Wwise.Event enableSound;
    public AK.Wwise.Event disableSound;

    public virtual void Start()
    {
        isPressed = false;
        if(GetComponent<Animator>() != null)
        {
            anim = GetComponent<Animator>();
        }
        
        triggerList = new List<Collider>();

        if (plateEnabled)
        {
            anim.Play("enable");
        }
    }

    public virtual void OnPressed()
    {
        if (!plateEnabled)
        {
            return;
        }
        isPressed = true;
        enableSound.Post(gameObject);
        pressedEvent.Invoke();
        anim.Play("press");
    }

    public virtual void OnUnpressed()
    {
        isPressed = false;
        disableSound.Post(gameObject);
        unpressedEvent.Invoke();
        anim.Play("unpress");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "DestroyableCube" && LayerMask.LayerToName(other.transform.parent.gameObject.layer) == "Interactable")
        {
            if (!triggerList.Contains(other))
            {
                triggerList.Add(other);
            }

            if (!isPressed)
            {
                OnPressed();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "DestroyableCube")
        {
            if (triggerList.Contains(other))
            {
                triggerList.Remove(other);
            }

            if(triggerList.Count == 0 && isPressed)
            {
                OnUnpressed();
            }
        }
    }

    public void EnablePlate()
    {
        plateEnabled = true;
        
        anim.Play("enable");
    }

    public void DisablePlate()
    {
        plateEnabled = false;
        
        anim.Play("disable");
    }
}
