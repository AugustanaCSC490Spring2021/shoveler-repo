using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float start;
    public Text timerText;

    // Start is called before the first frame update
    void Start()
    {
        timerText.text = start.ToString("F1");
    }

    // Update is called once per frame
    void Update()
    {
        start += Time.deltaTime;
        string minutes = ((int)start / 60).ToString();
        string seconds = (start % 60).ToString("f0");
        timerText.text = minutes + ":"+ seconds;
        
    }

    public float getTimer() {
        return start;
    }

    public void setTimer(float min) {
        start = min;
    }
}
