using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCoordinator : MonoBehaviour
{
    public static FireCoordinator Instance { get; private set; }

    private bool isProcessStarted;
    private Vector3 targetPosition;
    private Vector3 previousHitPosition;

    [SerializeField] private float radius;
    private float currentRadius;

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

    public void StartAdjustmentProcess(Vector3 target)
    {
        currentRadius = radius;
        isProcessStarted = true;
        targetPosition = target;

        Vector3 verticalPointInCirle = Random.insideUnitCircle * radius * 1.5f;
        Vector3 horizontalPointInCirle = new Vector3(verticalPointInCirle.x, 0, verticalPointInCirle.y);
        ArtilleryController.Instance.LaunchBullet(target + horizontalPointInCirle);
        previousHitPosition = target + horizontalPointInCirle;
    }

    public void SetHitPosition(Vector3 hitPosition)
    {
        if (!isProcessStarted)
            return;

        Debug.Log("distance " + Vector3.Distance(hitPosition, previousHitPosition));
        if (Vector3.Distance(hitPosition, previousHitPosition) < 15f)
            currentRadius *= 0.75f;
        else
            currentRadius *= 1.15f;

        currentRadius = Mathf.Clamp(currentRadius, 0f, radius * 1.5f);

        Vector3 verticalPointInCirle = Random.insideUnitCircle * currentRadius;
        Vector3 horizontalPointInCirle = new Vector3(verticalPointInCirle.x, 0, verticalPointInCirle.y);
        ArtilleryController.Instance.LaunchBullet(targetPosition + horizontalPointInCirle);
        previousHitPosition = targetPosition + horizontalPointInCirle;
    }

    public void StopAdjustmentProcess()
    {
        isProcessStarted = false;
    }
}
