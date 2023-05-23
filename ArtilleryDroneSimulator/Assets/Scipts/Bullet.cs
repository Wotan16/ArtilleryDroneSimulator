using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed;
    private float distance;
    private float currentDistance;
    private Vector3 directionVector;
    private Rigidbody myRigidbody;

    [SerializeField] private Transform explosionPrefab;

    private void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
        currentDistance = 0;
    }

    private void FixedUpdate()
    {
        currentDistance += speed * Time.fixedDeltaTime;
        float height = ArtilleryController.Instance.ballisticCurve.Evaluate(currentDistance/distance) * distance;
        Vector3 lenghtVector = directionVector * currentDistance;
        lenghtVector.y = height;
        Vector3 moveVector = ArtilleryController.Instance.transform.position + lenghtVector;
        myRigidbody.MovePosition(moveVector);

        if (currentDistance >= distance * 0.97f)
            speed = 10f;
    }

    public void SetParams(float distance, Vector3 targetPoint)
    {
        this.distance = distance;
        directionVector = (targetPoint - transform.position).normalized;
        directionVector.y = 0;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
