using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HittableTarget : Hittable
{
    Quaternion currentRotation;
    Quaternion endRotation;
    Quaternion initialRotation;
    public Vector3 rotationInEularAngles;
    public float timer;
    float elapsedTime;
    public bool canBeHit = true;
    bool moveTarget;
    // Start is called before the first frame update
    void Start()
    {
        initialRotation = transform.parent.rotation;
        currentRotation = transform.parent.rotation;
        resetAllowedToBeHitTimes = allowedToBeHitTimes;
        StartTimer.OnStartingTimer += ResetTarget;
    }
    protected override void ResetTarget()
    {
        allowedToBeHitTimes = resetAllowedToBeHitTimes;
        transform.parent.rotation = initialRotation;
        currentRotation = initialRotation;
        canBeHit = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (moveTarget)
        {
            MoveTarget();
        }
    }

    public void StartMovingTarget(Vector3 moveAmount)
    {
        endRotation = currentRotation * Quaternion.Euler(moveAmount);
        moveTarget = true;
        canBeHit = false;
    }

    void MoveTarget()
    {
        elapsedTime += Time.fixedDeltaTime;
        float t = elapsedTime / timer;
        transform.parent.rotation = Quaternion.Lerp(currentRotation, endRotation, t);
        if (t > 1)
        {
            moveTarget = false;
            canBeHit = true;
            currentRotation = transform.parent.rotation;
            elapsedTime = 0;
        }
    }

    public override void TakeHit(Vector3 direction, float force, Vector3 hitPoint)
    {
        if (canBeHit)
        {
            allowedToBeHitTimes--;
            StartMovingTarget(rotationInEularAngles);
        }
    }
    private void OnDestroy()
    {
        StartTimer.OnStartingTimer -= ResetTarget;
    }
}
