using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StartTimer : MonoBehaviour
{
    bool canCloseGate, timerStarted;

    public static Action OnClosingGates;
    public static Action OnStartingTimer;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.OnFailingRun += CanRetryCourse;
        GameManager.OnSucceedingRun += CanRetryCourse;
    }

    void CanRetryCourse()
    {
        Debug.Log("Can Retry the Course");
        timerStarted = false;
        canCloseGate = true;
    }

    private void OnDestroy()
    {
        GameManager.OnFailingRun -= CanRetryCourse;
        GameManager.OnSucceedingRun -= CanRetryCourse;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //Debug.Log("I am colliding with the player!");
            if (!timerStarted)
            {
                //Debug.Log("Start the timer!");
                timerStarted = true;
                OnStartingTimer?.Invoke();
                if (canCloseGate)
                {
                    //Debug.Log("close the gates!!!");
                    OnClosingGates?.Invoke();
                    canCloseGate = false;
                }
            }
        }
    }
}
