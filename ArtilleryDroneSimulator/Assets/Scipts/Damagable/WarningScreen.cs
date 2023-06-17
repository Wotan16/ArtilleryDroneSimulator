using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarningScreen : MonoBehaviour
{
    [SerializeField] private GameObject warningObject;

    private void Start()
    {
        Hide();
        Building.OnAnyBuildingDamaged += Show;
    }

    public void Hide()
    {
        warningObject.SetActive(false);
    }

    private void Show()
    {
        warningObject.SetActive(true);
    }
}
