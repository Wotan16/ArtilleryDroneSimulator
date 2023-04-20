using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class MenuController : MonoBehaviour
{
    private bool isMapActive = false;

    private void Start()
    {
        MapCamera.Instance.enabled = isMapActive;
    }

    public void OnMapButtonPressed(CallbackContext context)
    {
        if (!context.performed)
            return;

        isMapActive = !isMapActive;
        DroneLocomotion.Instance.enabled = !isMapActive;
        MapCamera.Instance.enabled = isMapActive;
    }
}
