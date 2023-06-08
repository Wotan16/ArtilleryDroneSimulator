using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class DroneLocomotion : MonoBehaviour
{
    public static DroneLocomotion Instance { get; private set; }

    [Header("Camera")]
    [SerializeField] private float cameraRotationSpeed;
    [SerializeField] private float minCameraAngle;
    [SerializeField] private float maxCameraAngle;
    private Vector3 rotation;

    [Header("Locomotion")]
    [SerializeField] private float horizontalRotationSpeed;
    [SerializeField] private float horizontalMoveSpeed;
    [SerializeField] private float verticalMoveSpeed;
    private Vector3 moveDirection;

    [Header("References")]
    [SerializeField] private Transform camVisual;

    private Rigidbody rb;
    private bool isFunctioningBlocked;

    private void Awake()
    {
        if(Instance != null)
        {
            Debug.Log("more than one Drone in scene");
            Destroy(gameObject);
            return;
        }
        Instance = this;

        rb = GetComponent<Rigidbody>();
        isFunctioningBlocked = false;
    }

    private void FixedUpdate()
    {
        HandleLocomotion();
    }

    private void LateUpdate()
    {
        HandleHorizontalRotation();
        HandleVerticalRotation();
    }
    private void HandleLocomotion()
    {
        if (moveDirection != Vector3.zero)
        {
            Vector3 direction = (transform.forward * moveDirection.z + transform.right * moveDirection.x).normalized;
            Vector3 moveVector = transform.position + direction * horizontalMoveSpeed * Time.fixedDeltaTime;
            moveVector += transform.up * moveDirection.y * verticalMoveSpeed * Time.fixedDeltaTime;
            rb.MovePosition(moveVector);
        }
    }

    private void HandleVerticalRotation()
    {
        Vector3 camVisualRotation = camVisual.rotation.eulerAngles;
        float angle = camVisualRotation.x;
        if (rotation.x != 0f)
        {
            angle += rotation.x * cameraRotationSpeed;
        }

        angle = Mathf.Clamp(angle, minCameraAngle, maxCameraAngle);


        camVisual.rotation = Quaternion.Euler(new Vector3(angle, camVisualRotation.y, camVisualRotation.z));
    }

    private void HandleHorizontalRotation()
    {
        if (rotation != Vector3.zero)
        {
            Vector3 newRotation = transform.rotation.eulerAngles + new Vector3(0,
                rotation.y * horizontalRotationSpeed * 1f);
            rb.MoveRotation(Quaternion.Euler(newRotation));
        }
    }

    public void SetFunctioningBlocked(bool value)
    {
        isFunctioningBlocked = value;
    }

    #region InputEvents
    public void OnHorizontalMove(CallbackContext context)
    {
        if (isFunctioningBlocked)
        {
            moveDirection = Vector3.zero;
            return;
        }

        Vector2 vector = context.ReadValue<Vector2>();
        moveDirection.x = vector.x;
        moveDirection.z = vector.y;
    }

    public void OnVerticalMove(CallbackContext context)
    {
        if (isFunctioningBlocked)
        {
            moveDirection = Vector3.zero;
            return;
        }
        moveDirection.y = context.ReadValue<float>();
    }

    public void OnRotate(CallbackContext context)
    {
        if (isFunctioningBlocked)
        {
            rotation = Vector3.zero;
            return;
        }
        Vector2 vector = context.ReadValue<Vector2>();
        rotation.y = vector.x;
        rotation.x = -vector.y;
    }
    #endregion
}
