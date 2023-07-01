using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hittable : MonoBehaviour
{
    Rigidbody rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    public virtual void TakeHit(Vector3 direction, float force, Vector3 hitPoint)
    {
        //Debug.Log("virtual TakeHit");
        if (rb != null)
        {
            rb.AddForceAtPosition(direction * force, hitPoint);
            rb.isKinematic = false;
        }
    }
}
