using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerOverTime : MonoBehaviour
{
    [SerializeField] private float timeBetweenSpawn;
    [SerializeField] private float radius;
    [SerializeField] private Transform prefabToSpawn;
    [SerializeField] private bool spawnPrefabs;

    private void Start()
    {
        StartCoroutine(ExplosionSpawn());
    }

    private IEnumerator ExplosionSpawn()
    {
        while (true)
        {
            if (!spawnPrefabs)
                continue;

            yield return new WaitForSeconds(timeBetweenSpawn);
            Vector3 verticalPointInCirle = Random.insideUnitCircle * radius;
            Vector3 horizontalPointInCirle = new Vector3(verticalPointInCirle.x, 0, verticalPointInCirle.y);
            Vector3 spawnPosition = horizontalPointInCirle + transform.position;
            Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);
        }
    }
}
