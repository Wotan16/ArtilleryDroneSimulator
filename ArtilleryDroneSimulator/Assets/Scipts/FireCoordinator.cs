using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCoordinator : MonoBehaviour
{
    public static FireCoordinator Instance { get; private set; }

    private Vector3 targetPosition;
    private Vector3 previousHitPosition;

    [SerializeField] private float radius;
    [SerializeField] private float mistakePower;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.Log("more than one FireCoordinator in scene");
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void FireFirstProjectile(Vector3 targetPoint)
    {
        Vector3 verticalPointInCirle = Random.insideUnitCircle * radius;
        Vector3 horizontalPointInCirle = new Vector3(verticalPointInCirle.x, 0, verticalPointInCirle.y);
        ArtilleryController.Instance.LaunchBullet(targetPoint + horizontalPointInCirle);
        previousHitPosition = targetPoint + horizontalPointInCirle;
    }

    public void SetHitPosition(Vector3 hitPosition)
    {
        Vector3 corectionVector = targetPosition - hitPosition;
        corectionVector += Random.Range(-mistakePower, mistakePower) * corectionVector;
        Vector3 firePosition = previousHitPosition + corectionVector;
        ArtilleryController.Instance.LaunchBullet(firePosition);
        previousHitPosition = firePosition;
    }

    public void StopAdjustmentProcess()
    {
        targetPosition = Vector3.zero;
        previousHitPosition = Vector3.zero;
    }

    public void SetTarget(Vector3 target)
    {
        targetPosition = target;
    }
}

