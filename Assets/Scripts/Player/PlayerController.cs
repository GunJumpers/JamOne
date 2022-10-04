using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Serialized Variables")]
    [SerializeField] private Rigidbody _rigidbody;

    [Header("Current Player State")]
    public Vector3 moveDirection;
    private Vector3 _currentVelocity;

    [Header("Base Movement Stats")]
    [SerializeField] private float _maximumSpeed;
    [SerializeField] private float _movementAcceleration;
    [SerializeField] private float _movementDrag;
    

    // Start is called before the first frame update
    void Start()
    {
        InitializeRigidbody();
    }

    // Update is called once per frame
    void Update()
    {
        LimitMovement();
    }

    private void FixedUpdate()
    {
        ApplyMovement();
    }

    void InitializeRigidbody()
    {
        _rigidbody.drag = _movementDrag;
    }

    void ApplyMovement()
    {
        _rigidbody.AddForce(moveDirection * _movementAcceleration);
    }

    void LimitMovement()
    {
        _rigidbody.velocity = Vector3.ClampMagnitude(_rigidbody.velocity, _maximumSpeed);
    }



}
