using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamagable
{
    public void OnDamaged()
    {
        Destroy(gameObject);
    }
}
