using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InputMenuController : MonoBehaviour
{
    [SerializeField] private Button coordinatesButton;
    [SerializeField] private TMP_InputField XField;
    [SerializeField] private TMP_InputField YField;
    [SerializeField] private LayerMask terrainMask;

    private void Start()
    {
        coordinatesButton.onClick.AddListener(Fire);
    }

    private void Fire()
    {
        if (!float.TryParse(XField.text, out float x))
            return;

        if (!float.TryParse(YField.text, out float z))
            return;

        Physics.Raycast(new Vector3(x, 50f, z), Vector3.down, out RaycastHit hit, 200f, terrainMask);

        ArtilleryController.Instance.LaunchBullet(hit.point);
    }
}
