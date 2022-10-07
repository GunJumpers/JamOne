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
    public bool isInspecting;
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

    public override void Awake()
    {
        base.Awake();
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
        if (isInspecting)
        {
            ApplyInspect();
        }
        else
        {
            ApplyLook();
        }
        
        LimitMovement();
        RaycastGrabbablePivot();
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

    private void ApplyInspect()
    {
        if(currentGrabbable == null) {
            return;
        }
        Vector2 looking = GetPlayerLook();
        float lookX = looking.x * _lookSensitivity * Time.deltaTime;
        float lookY = looking.y * _lookSensitivity * Time.deltaTime;

        currentGrabbable.transform.Rotate((Vector3.up * lookX) + (Vector3.forward * lookY));

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

    void TryInteract()
    {
        RaycastHit info;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out info, 2, interactableLayers))
        {
            if(LayerMask.LayerToName(info.transform.gameObject.layer) == "Interactable")
            {
                info.transform.GetComponent<Interactable>().InteractAction();
            }
        }

    }

    void ApplyGrab()
    {
        if(currentGrabbable != null)
        {
            float grabbableOffset = Vector3.Distance(currentGrabbable.transform.position, _grabPivot.transform.position);
            Vector3 forceDirection = (_grabPivot.position - currentGrabbable.transform.position).normalized * grabbableForce * grabbableOffset;
            if (!currentGrabbable.isLocked)
            {
                currentGrabbable.GetRigidbody().AddForce(forceDirection, ForceMode.Acceleration);
                if (grabbableOffset < 0.5f)
                {
                    currentGrabbable.transform.position = _grabPivot.position;
                    currentGrabbable.LockGrabbable();
                    currentGrabbable.transform.SetParent(_grabPivot);
                }
            }
            
        }
    }

    void RaycastGrabbablePivot()
    {
        RaycastHit info;
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        float offset = interactableDistance;

        if(currentGrabbable != null)
        {
            offset += currentGrabbable.offset;
        }


        if (Physics.Raycast(ray, out info, offset, interactableLayers))
        {
            float newDistance = info.distance;
            if (currentGrabbable != null)
            {
                newDistance -= currentGrabbable.offset;
            }
             

            _grabPivot.position = ray.GetPoint(newDistance);
        }
        else
        {
            _grabPivot.position = ray.GetPoint(interactableDistance);
        }
    }

    public void SetCurrentGrabbable(Grabbable grabbable)
    {
        currentGrabbable = grabbable;
    }

    public void OnInteract()
    {
        if(currentGrabbable == null)
        {
            TryInteract();
        }
        else
        {
            currentGrabbable.IsNoLongerBeingGrabbed();
        }
        
    }

    public void OnInspect(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            isInspecting = true;
        }
        if (context.canceled)
        {
            isInspecting = false;
        }
    }


}
