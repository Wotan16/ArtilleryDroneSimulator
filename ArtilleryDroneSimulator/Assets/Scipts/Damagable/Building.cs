using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour, IDamagable
{
    public static Action OnAnyBuildingDamaged;
    public void OnDamaged()
    {
        OnAnyBuildingDamaged?.Invoke();
    }
}
