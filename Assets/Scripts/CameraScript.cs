using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    // Update is called once per frame
    
    float xRotation;
    public float mouseSensitivity = 1f;
    private void FixedUpdate()
    {
        float mouseY = Input.GetAxisRaw("Mouse Y") * mouseSensitivity * 100f;
        xRotation -= mouseY * Time.deltaTime;
        xRotation = Mathf.Clamp(xRotation, -45f, 45f);
        transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
    }
}
