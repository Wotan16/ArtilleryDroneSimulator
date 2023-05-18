using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtilleryController : MonoBehaviour
{
    public static ArtilleryController Instance { get; private set; }

    public AnimationCurve curve;
    [SerializeField] private Transform bulletPrefab;

    public Transform target;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.Log("more than one ArtilleryController in scene");
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            LaunchBullet(target.position);
        }
    }

    private void LaunchBullet(Vector3 targetPoint)
    {
        float distance = (targetPoint - transform.position).magnitude;
        Transform bulletTransform = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        Bullet bullet = bulletTransform.GetComponent<Bullet>();
        bullet.SetParams(curve, distance, targetPoint);
    }
}
