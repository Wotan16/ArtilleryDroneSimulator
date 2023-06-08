using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InputMenuController : MonoBehaviour
{
    [SerializeField] private Button targetButton;
    [SerializeField] private Button hitButton;
    [SerializeField] private Button eliminateButton;
    [SerializeField] private TMP_InputField XField;
    [SerializeField] private TMP_InputField YField;
    [SerializeField] private LayerMask terrainMask;

    private void Start()
    {
        targetButton.onClick.AddListener(() =>
        {
            targetButton.gameObject.SetActive(false);
            hitButton.gameObject.SetActive(true);
            eliminateButton.gameObject.SetActive(true);
            FireCoordinator.Instance.StartAdjustmentProcess(Fire());
        });

        hitButton.onClick.AddListener(() =>
        {
            FireCoordinator.Instance.SetHitPosition(Fire());
        });

        eliminateButton.onClick.AddListener(() =>
        {
            targetButton.gameObject.SetActive(true);
            hitButton.gameObject.SetActive(false);
            eliminateButton.gameObject.SetActive(false);
            FireCoordinator.Instance.StopAdjustmentProcess();
        });
    }

    private Vector3 Fire()
    {
        if (!float.TryParse(XField.text, out float x))
            return Vector3.zero;

        if (!float.TryParse(YField.text, out float z))
            return Vector3.zero;

        Physics.Raycast(new Vector3(x, 50f, z), Vector3.down, out RaycastHit hit, 200f, terrainMask);

        return hit.point;
    }
}
