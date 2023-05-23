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

    private void Start()
    {
        coordinatesButton.onClick.AddListener(Fire);
    }

    private void Fire()
    {
        if (!float.TryParse(XField.text, out float x))
            return;

        if (!float.TryParse(XField.text, out float z))
            return;

        Vector3 target = new Vector3(x, ArtilleryController.Instance.transform.position.y, z);

        ArtilleryController.Instance.LaunchBullet(target);
    }
}
