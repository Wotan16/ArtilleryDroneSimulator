using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class MapCamera : MonoBehaviour
{
    public static MapCamera Instance { get; private set; }

    [SerializeField] private float minSize;
    [SerializeField] private float maxSize;
    [SerializeField] private float speed;
    [SerializeField] private float rotationSpeed;

    [SerializeField] private Transform mapBottomLeftTransform;
    [SerializeField] private Transform mapUpRightTransform;
    [SerializeField] private Transform worldUpRightTransform;
    [SerializeField] private LayerMask mapMask;
    private Camera mapCamera;

    private Vector2 moveDirection;

    [SerializeField] private float sightBaseSize;
    [SerializeField] private GameObject mapUIObject;
    [SerializeField] private Transform sightTransform;

    private float rotationDirection;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.Log("more than one Map in scene");
            Destroy(gameObject);
            return;
        }
        Instance = this;

        mapCamera = GetComponent<Camera>();
    }

    private void Update()
    {
        MoveCamera();
    }

    private void FixedUpdate()
    {
        Rotate();
    }

    private void Rotate()
    {
        if (rotationDirection == 0)
            return;

        float value = rotationSpeed * rotationDirection;
        Vector3 rotation = transform.rotation.eulerAngles + new Vector3(0, 0, value);
        transform.rotation = Quaternion.Euler(rotation);
    }

    private void MoveCamera()
    {
        if (moveDirection == Vector2.zero)
            return;

        Vector3 rotation = transform.rotation.eulerAngles;
        rotation.x = 0;
        Vector3 moveVector = Quaternion.Euler(rotation) * new Vector3(moveDirection.x, 0, moveDirection.y);
        Vector3 newPosition = transform.position + moveVector * speed * Time.deltaTime;

        newPosition.x = Mathf.Clamp(newPosition.x, mapBottomLeftTransform.position.x, mapUpRightTransform.position.x);
        newPosition.z = Mathf.Clamp(newPosition.z, mapBottomLeftTransform.position.z, mapUpRightTransform.position.z);

        transform.position = newPosition;
    }

    public Vector3 ConvertMapToWorldPoint(Vector3 mapPoint)
    {
        float worldX = worldUpRightTransform.position.x * mapPoint.x / mapUpRightTransform.position.x;
        float worldZ = worldUpRightTransform.position.z * mapPoint.z / mapUpRightTransform.position.z;
        return new Vector3(worldX, worldUpRightTransform.position.y, worldZ);
    }

    #region InputEvents
    public void OnMove(CallbackContext context)
    {
        moveDirection = context.ReadValue<Vector2>();
    }

    public void OnSizeChange(CallbackContext context)
    {
        if (!context.performed)
            return;

        float newSize = mapCamera.orthographicSize;
        newSize += 0.25f * context.ReadValue<float>();
        newSize = Mathf.Clamp(newSize, minSize, maxSize);
        mapCamera.orthographicSize = newSize;

        sightTransform.localScale = new Vector3(sightBaseSize * newSize, sightBaseSize * newSize, 1);
    }

    public void OnRotate(CallbackContext context)
    {
        if (context.canceled)
        {
            rotationDirection = 0;
            return;
        }

        rotationDirection = context.ReadValue<float>();
    }
    #endregion
}
