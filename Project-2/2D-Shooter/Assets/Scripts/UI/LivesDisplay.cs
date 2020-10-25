using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LivesDisplay : UIelement
{
    [Tooltip("The text UI to use for display")]
    public Text displayText = null;
    public Health PlayerHealth = null;

    public void DisplayLives()
    {
        if (displayText != null)
        {
            displayText.text = "Lives: " + PlayerHealth.currentLives.ToString();
        }
    }

    private void Update()
    {
        DisplayLives();
    }
}
