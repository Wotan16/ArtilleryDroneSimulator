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

    [SerializeField] private Transform mapBottomLeftTransform;
    [SerializeField] private Transform mapUpRightTransform;
    [SerializeField] private Transform worldUpRightTransform;
    [SerializeField] private LayerMask mapMask;
    private Camera mapCamera;

    private Vector2 moveDirection;

    [SerializeField] private GameObject mapUIObject;

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

    private void MoveCamera()
    {
        if (moveDirection == Vector2.zero)
            return;

        Vector3 newPosition = transform.position + new Vector3(moveDirection.x, 0, moveDirection.y) * speed * Time.deltaTime;

        newPosition.x = Mathf.Clamp(newPosition.x, mapBottomLeftTransform.position.x + mapCamera.orthographicSize * 1.8f,
            mapUpRightTransform.position.x - mapCamera.orthographicSize * 1.8f);
        newPosition.z = Mathf.Clamp(newPosition.z, mapBottomLeftTransform.position.z + mapCamera.orthographicSize,
            mapUpRightTransform.position.z - mapCamera.orthographicSize);

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
        float newSize = mapCamera.orthographicSize;
        newSize += 0.25f * context.ReadValue<float>();
        newSize = Mathf.Clamp(newSize, minSize, maxSize);
        mapCamera.orthographicSize = newSize;
    }
    #endregion

    private void OnEnable()
    {
        mapUIObject.SetActive(true);
    }

    private void OnDisable()
    {
        mapUIObject.SetActive(false);
    }
}
