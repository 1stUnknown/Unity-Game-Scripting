using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialText : MonoBehaviour
{
    TextMeshProUGUI text;
    public string[] tutorials;
    int amountOfTutorialTexts;
    int currentTutorialText;
    float initiationTime;
    public float timer;
    bool alreadyDestroyed;

    // Start is called before the first frame update
    void Start()
    {
        amountOfTutorialTexts = tutorials.Length;

        text = GetComponent<TextMeshProUGUI>();
        LoadNextTutorial.OnCollisionPlayer += UpdateTutorialText;
        ChangeTutorialText();
    }

    private void FixedUpdate()
    {
        if (Time.time > initiationTime)
        {
            text.text = "";
        }
    }

    void UpdateTutorialText()
    {
        currentTutorialText++;
        if (currentTutorialText >= amountOfTutorialTexts)
        {
            LoadNextTutorial.OnCollisionPlayer -= UpdateTutorialText;
            alreadyDestroyed = true;
        }
        else
        {
            ChangeTutorialText();
        }
    }
    void ChangeTutorialText()
    {
        text.text = tutorials[currentTutorialText];
        initiationTime = Time.time + timer;
    }
    private void OnDestroy()
    {
        if (!alreadyDestroyed)
        {
            LoadNextTutorial.OnCollisionPlayer -= UpdateTutorialText;
        }
    }
}
