using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeDisplay : MonoBehaviour
{

    [Tooltip("The text to use when displaying the score")]
    public Text displayText = null;

    private float currentTime = 0f;

    void Update()
    {
        currentTime += Time.deltaTime;

        displayText.text = "Time: " + currentTime.ToString("0.00");
    }
}
