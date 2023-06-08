using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Menu
{
    public class CameraLocomotion : MonoBehaviour
    {
        [SerializeField] private float speed;

        private void Update()
        {
            transform.position += transform.right * -1 * Time.fixedDeltaTime * speed;
        }
    }
}
