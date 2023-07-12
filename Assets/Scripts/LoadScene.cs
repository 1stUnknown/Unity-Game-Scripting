using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public string sceneToBeLoaded;

    public void LoadAScene(string sceneToBeLoaded)
    {
        SceneManager.LoadScene(sceneToBeLoaded);
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            SceneManager.LoadScene(sceneToBeLoaded);
    }
}
