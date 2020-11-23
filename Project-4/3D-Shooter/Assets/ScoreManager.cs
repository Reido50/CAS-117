using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public WaveManager manager;
    public Text text;
    // Update is called once per frame
    void Update()
    {
        text.text = "Wave: " + (manager.waveNum-1);
    }
}
