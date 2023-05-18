using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed;
    private AnimationCurve curve;
    private float distance;
    private float currentDistance;
    private Vector3 directionVector;
    private Rigidbody myRigidbody;
    private float Height = 0;

    private void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
        currentDistance = 0;
    }

    private void Update()
    {
        currentDistance += speed * Time.deltaTime;
        float height = ArtilleryController.Instance.curve.Evaluate(currentDistance/distance) * distance;
        Height = height;
        Vector3 lenghtVector = directionVector * currentDistance;
        lenghtVector.y = height;
        Vector3 moveVector = ArtilleryController.Instance.transform.position + lenghtVector;
        myRigidbody.MovePosition(moveVector);
    }

    public void SetParams(AnimationCurve curve, float distance, Vector3 targetPoint)
    {
        this.curve = curve;
        this.distance = distance;
        directionVector = (targetPoint - transform.position).normalized;
        directionVector.y = 0;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
