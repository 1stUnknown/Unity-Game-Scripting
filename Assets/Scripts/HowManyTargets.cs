using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HowManyTargets : MonoBehaviour
{
    public static UnityEvent<int> OnHowManyTargetsThereAre;
    // Awake is called before Start
    void Awake()
    {
        OnHowManyTargetsThereAre = new UnityEvent<int>();
        Invoke("UpdateHowManyTargetsThereAre", 0.1f);
    }
    void UpdateHowManyTargetsThereAre()
    {
        OnHowManyTargetsThereAre?.Invoke(transform.childCount);
    }
}
