using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CourseGate : MonoBehaviour
{
    Vector3 currentGatePosition;
    public Vector3 moveGateTo;
    public float timer;
    float elapsedTime;
    bool canRetry, moving;
    public bool isRetryGate;
    void Start()
    {
        currentGatePosition = transform.position;
        if (isRetryGate)
            GameManager.OnFailingRun += OpenGate;

        GameManager.OnSucceedingRun += OpenGate;
        StartTimer.OnClosingGates += CloseGate;
    }

    protected void OpenGate()
    {
        Debug.Log("OpenGate() is called");
        canRetry = true;
        moving = true;
    }

    protected void CloseGate()
    {
        Debug.Log("CloseGate() is called");
        canRetry = false;
        moving = true;
    }

    protected void MoveGate(Vector3 desiredGatePosition)
    {
        elapsedTime += Time.fixedDeltaTime;
        float t = elapsedTime / timer;
        transform.position = Vector3.Lerp(currentGatePosition, desiredGatePosition, t);
        if (t > 1)
        {
            moving = false;
            currentGatePosition = transform.position;
            elapsedTime = 0;
        }
    }
    void FixedUpdate()
    {
        if (moving)
        {
            if (canRetry)
            {
                MoveGate(currentGatePosition + moveGateTo);
            }
            else
            {
                MoveGate(currentGatePosition - moveGateTo);
            }
        }
    }

    private void OnDestroy()
    {
        if (isRetryGate)
            GameManager.OnFailingRun -= OpenGate;
        GameManager.OnSucceedingRun -= OpenGate;
        StartTimer.OnClosingGates -= CloseGate;
    }
}
