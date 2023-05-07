using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MapCoordinates : MonoBehaviour
{
    [SerializeField] private TextMeshPro coordinates;
    [SerializeField] private Transform sightTransform;

    private void Update()
    {
        Vector3 sightPosition = MapCamera.Instance.ConvertMapToWorldPoint(sightTransform.position);
        coordinates.text = "X:" + sightPosition.x + " Y:" + sightPosition.z;
    }
}
