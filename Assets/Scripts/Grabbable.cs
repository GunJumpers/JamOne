using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabbable : Interactable
{
    public Rigidbody _rb;
    public PlayerController playerController;
    public bool isLocked;
    public float offset;

    public override void Start()
    {
        base.Start();
        if(GetComponent<Rigidbody>() != null)
        {
            _rb = GetComponent<Rigidbody>();
        }

    }

    public override void InteractAction()
    {
        base.InteractAction();
        IsBeingGrabbed();
    }

    public void IsBeingGrabbed()
    {
        PlayerController.Instance.SetCurrentGrabbable(this);
        _rb.constraints = RigidbodyConstraints.FreezeRotation;
        _rb.useGravity = false;
        gameObject.layer = 2;
    }

    public void IsNoLongerBeingGrabbed()
    {
        if(PlayerController.Instance.currentGrabbable == this)
        {
            UnlockGrabbable();
            PlayerController.Instance.currentGrabbable = null;
            _rb.constraints = RigidbodyConstraints.None;
            _rb.useGravity = true;
            gameObject.layer = 9;
        }
    }

    public void LockGrabbable()
    {
        isLocked = true;
        _rb.isKinematic = true;
    }

    public void UnlockGrabbable()
    {
        if (!isLocked)
        {
            return;
        }

        isLocked = false;
        _rb.isKinematic = false;
        transform.parent = null;

    }

    public Rigidbody GetRigidbody()
    {
        return _rb;
    }

}
