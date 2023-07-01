using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HittableTarget : Hittable
{
    Quaternion currentRotation;
    Quaternion endRotation;
    public Vector3 rotationInEularAngles;
    Transform parentTransform;
    public float timer;
    float elapsedTime;
    public bool canBeHit;
    bool wasHit;
    // Start is called before the first frame update
    void Start()
    {
        parentTransform = transform.parent;
        currentRotation = parentTransform.rotation;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (wasHit)
        {
            elapsedTime += Time.fixedDeltaTime;
            float t = elapsedTime / timer;
            parentTransform.rotation = Quaternion.Lerp(currentRotation, endRotation, t);
            if (t > 1)
            {
                wasHit = false;
                canBeHit = true;
                currentRotation = parentTransform.rotation;
                elapsedTime = 0;
            }
        }
    }

    public override void TakeHit(Vector3 direction, float force, Vector3 hitPoint)
    {
        //Debug.Log("Override TakeHit");
        if (canBeHit)
        {
            endRotation = currentRotation * Quaternion.Euler(rotationInEularAngles);
            wasHit = true;
            canBeHit = false;
        }
    }
}
