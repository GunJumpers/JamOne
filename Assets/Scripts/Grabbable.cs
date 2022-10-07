using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabbable : Interactable
{
    private Rigidbody _rb;

    public override void Start()
    {
        base.Start();
        if(GetComponent<Rigidbody>() != null)
        {
            _rb = GetComponent<Rigidbody>();
        }
        
    }

    public void IsBeingGrabbed()
    {
        PlayerController.Instance.currentGrabbable = this;
        _rb.constraints = RigidbodyConstraints.FreezeRotation;
    }

    public void IsNoLongerBeingGrabbed()
    {
        if(PlayerController.Instance.currentGrabbable == this)
        {
            PlayerController.Instance.currentGrabbable = null;
        }
    }

    public Rigidbody GetRigidbody()
    {
        return _rb;
    }

}
