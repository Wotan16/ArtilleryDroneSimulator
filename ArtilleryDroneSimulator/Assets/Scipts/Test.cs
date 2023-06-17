using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public float radius;
    public LayerMask mask;

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.G))
        //{
        //    Collider[] colls = Physics.OverlapSphere(transform.position, radius, mask);
        //    foreach(Collider coll in colls)
        //    {
        //        Debug.Log(coll.name);
        //        coll.GetComponent<Enemy>().Die();
        //    }
        //}
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
