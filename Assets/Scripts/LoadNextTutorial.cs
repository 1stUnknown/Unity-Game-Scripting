using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class LoadNextTutorial : MonoBehaviour
{
    public static event Action OnCollisionPlayer;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnCollisionPlayer?.Invoke();
            Destroy(gameObject);
        }
    }
}
