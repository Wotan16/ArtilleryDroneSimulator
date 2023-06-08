using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class MenuController : MonoBehaviour
{
    private bool isMapActive = true;
    [SerializeField] private GameObject mapUIObject;

    private void Start()
    {
        MapCamera.Instance.enabled = isMapActive;
        ChangeState();
    }

    private void ChangeState()
    {
        isMapActive = !isMapActive;
        DroneLocomotion.Instance.enabled = !isMapActive;
        MapCamera.Instance.gameObject.SetActive(isMapActive);
        mapUIObject.SetActive(isMapActive);
    }

    public void OnMapButtonPressed(CallbackContext context)
    {
        if (!context.performed)
            return;

        ChangeState();
    }
}
