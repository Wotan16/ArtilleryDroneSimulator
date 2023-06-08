using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteAfterTime : MonoBehaviour
{
    [SerializeField] private float timeToDestroy;

    private void Start()
    {
        StartCoroutine(Delete(timeToDestroy));
    }

    private IEnumerator Delete(float time)
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}
