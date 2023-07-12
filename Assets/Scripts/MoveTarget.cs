using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTarget : MonoBehaviour
{
    // Start is called before the first frame update
    public HittableTarget[] targets;
    public Vector3 moveAmount;
    bool canMoveTargets;

    private void Start()
    {
        StartTimer.OnStartingTimer += CanMoveTargetAgain;
        canMoveTargets = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if (targets != null && canMoveTargets)
            {
                for (int target = 0; target < targets.Length; target++)
                {
                    targets[target].StartMovingTarget(moveAmount);
                }
                canMoveTargets = false;
            }
        }
    }

    void CanMoveTargetAgain()
    {
        canMoveTargets = true;
    }
    private void OnDestroy()
    {
        StartTimer.OnStartingTimer -= CanMoveTargetAgain;
    }
}
