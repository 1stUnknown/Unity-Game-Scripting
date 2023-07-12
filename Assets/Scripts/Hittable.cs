using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hittable : MonoBehaviour
{
    Rigidbody rb;
    public int allowedToBeHitTimes = 1;
    protected int resetAllowedToBeHitTimes;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        resetAllowedToBeHitTimes = allowedToBeHitTimes;
    }
    protected virtual void ResetTarget()
    {
        allowedToBeHitTimes = resetAllowedToBeHitTimes;
    }
    public virtual void TakeHit(Vector3 direction, float force, Vector3 hitPoint)
    {
        allowedToBeHitTimes--;
        if (rb != null)
        {
            rb.AddForceAtPosition(direction * force, hitPoint);
            rb.isKinematic = false;
        }
    }
}
