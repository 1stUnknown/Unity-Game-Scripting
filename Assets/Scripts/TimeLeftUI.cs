using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeLeftUI : MonoBehaviour
{
    TextMeshProUGUI text;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        GameManager.OnTimeLeft.AddListener(DisplayTimeLeft);
    }

    void DisplayTimeLeft(float amount)
    {
        Debug.Log("Update the timer!");
        text.text = "" + amount;
    }
    // Update is called once per frame
    private void OnDestroy()
    {
        GameManager.OnTimeLeft.RemoveListener(DisplayTimeLeft);
    }
}
