using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAroundEmpty : MonoBehaviour
{
    // Start is called before the first frame update
    Quaternion currentRotation;
    Quaternion endRotation;
    public Vector3 rotationInEularAngles;
    public float timer;
    float elapsedTime;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Rotate()
    {
        elapsedTime += Time.fixedDeltaTime;
        float t = elapsedTime / timer;
        transform.rotation = Quaternion.Lerp(currentRotation, endRotation, t);
        if (t > 1)
        {

            /*wasHit = false;
            canBeHit = true;*/
            currentRotation = transform.rotation;
            elapsedTime = 0;
        }
    }
}
