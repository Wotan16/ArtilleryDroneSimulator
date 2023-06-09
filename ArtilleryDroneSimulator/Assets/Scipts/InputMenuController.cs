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
    [SerializeField] private Button correctionFireButton;

    [SerializeField] private TMP_InputField XField;
    [SerializeField] private TMP_InputField YField;

    [SerializeField] private LayerMask terrainMask;

    private AdjustmentState currentState;

    private void Start()
    {
        currentState = AdjustmentState.WaitingForTarget;
        HideAllButtons();
        targetButton.gameObject.SetActive(true);
        correctionFireButton.gameObject.SetActive(true);

        targetButton.onClick.AddListener(() =>
        {
            HideAllButtons();
            hitButton.gameObject.SetActive(true);
            eliminateButton.gameObject.SetActive(true);
            FireCoordinator.Instance.SetTarget(GetPointFromMenu());
            switch (currentState)
            {
                case AdjustmentState.WaitingForTarget:
                    currentState = AdjustmentState.AimingInTarget;
                    FireCoordinator.Instance.FireFirstProjectile(GetPointFromMenu());
                    return;
            }
        });

        hitButton.onClick.AddListener(() =>
        {
            HideAllButtons();
            hitButton.gameObject.SetActive(true);
            eliminateButton.gameObject.SetActive(true);
            FireCoordinator.Instance.SetHitPosition(GetPointFromMenu());
        });

        eliminateButton.onClick.AddListener(() =>
        {
            HideAllButtons();
            targetButton.gameObject.SetActive(true);
            correctionFireButton.gameObject.SetActive(true);
            currentState = AdjustmentState.WaitingForTarget;
            FireCoordinator.Instance.StopAdjustmentProcess();
        });

        correctionFireButton.onClick.AddListener(() =>
        {
            HideAllButtons();
            currentState = AdjustmentState.CorrectionHit;
            targetButton.gameObject.SetActive(true);
            eliminateButton.gameObject.SetActive(true);
            FireCoordinator.Instance.FireFirstProjectile(GetPointFromMenu());
        });
    }

    private Vector3 GetPointFromMenu()
    {
        if (!float.TryParse(XField.text, out float x))
            return Vector3.zero;

        if (!float.TryParse(YField.text, out float z))
            return Vector3.zero;

        Physics.Raycast(new Vector3(x, 50f, z), Vector3.down, out RaycastHit hit, 200f, terrainMask);

        return hit.point;
    }

    private void HideAllButtons()
    {
        foreach(Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }

        XField.gameObject.SetActive(true);
        YField.gameObject.SetActive(true);
    }
}

public enum AdjustmentState
{
    WaitingForTarget,
    AimingInTarget,
    CorrectionHit
}
