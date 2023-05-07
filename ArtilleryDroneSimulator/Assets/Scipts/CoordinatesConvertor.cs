using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoordinatesConvertor : MonoBehaviour
{
    public static CoordinatesConvertor Instance { get; private set; }

    

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.Log("more than one Convertor in scene");
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    
}
