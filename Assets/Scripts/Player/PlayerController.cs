using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class PlayerController : UnitySingleton<PlayerController>
{
    [Header("Serialized Variables")]
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private Transform _playerCamera;
    [SerializeField] private CinemachineVirtualCamera _vcam;

    [Header("Current Player State")]
    public Vector3 moveDirection;
    public Vector2 lookDirection;

    private Vector3 _currentVelocity;
    private float xRotation = 0f;

    [Header("Interaction System")]
    [SerializeField] private Transform _grabPivot;
    public Grabbable currentGrabbable;
    public float grabbableForce;
    public float interactableDistance;
    public LayerMask interactableLayers;

    [Header("Base Movement Stats [Reset on Play]")]
    [SerializeField] private float _maximumSpeed;
    [SerializeField] private float _movementAcceleration;
    [SerializeField] private float _movementDrag;
    [SerializeField] private float _FOV;
    [SerializeField] private float _lookSensitivity = 1.0f;

    private void Awake()
    {
        _vcam.m_Lens.FieldOfView = _FOV;
    }
    // Start is called before the first frame update
    void Start()
    {
        InitializeRigidbody();
    }

    // Update is called once per frame
    void Update()
    {
        ApplyLook();
        LimitMovement();
        ApplyGrab();
    }

    private void FixedUpdate()
    {
        ApplyMovement();
    }

    private void ApplyLook()
    {
        Vector2 looking = GetPlayerLook();
        float lookX = looking.x * _lookSensitivity * Time.deltaTime;
        float lookY = looking.y * _lookSensitivity * Time.deltaTime;

        xRotation -= lookY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        _playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        transform.Rotate(Vector3.up * lookX);
    }


    void InitializeRigidbody()
    {
        _rigidbody.drag = _movementDrag;
    }

    void ApplyMovement()
    {
        Vector2 movement = GetPlayerMovement();
        Vector3 move = transform.right * movement.x + transform.forward * movement.y;

        _rigidbody.AddForce(move * _movementAcceleration);
    }

    void LimitMovement()
    {
        _rigidbody.velocity = Vector3.ClampMagnitude(_rigidbody.velocity, _maximumSpeed);
    }

    public Vector2 GetPlayerMovement()
    {
        return moveDirection;
    }

    public Vector2 GetPlayerLook()
    {
        return lookDirection;
    }

    public void Look(InputAction.CallbackContext context)
    {
        lookDirection = context.ReadValue<Vector2>();
        
    }

    public void Move(InputAction.CallbackContext context)
    {
        moveDirection = context.ReadValue<Vector2>();

    }

    void TryGrab()
    {
        RaycastHit info;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out info, 1, interactableLayers))
        {

        }

    }

    void ApplyGrab()
    {
        if(currentGrabbable != null)
        {
            Vector3 forceDirection = (_grabPivot.position - currentGrabbable.transform.position).normalized * grabbableForce;

            currentGrabbable.GetRigidbody().AddForce(forceDirection, ForceMode.Force);
        }
    }


}
