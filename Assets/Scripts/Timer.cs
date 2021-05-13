using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float start;
    public Text textBox;

    // Start is called before the first frame update
    void Start()
    {
        textBox.text = start.ToString("F1");
    }

    // Update is called once per frame
    void Update()
    {
        start += Time.deltaTime;
        textBox.text = start.ToString("F1");
    }

    public float getTimer() {
        return start;
    }

    public void setTimer() {
        start = 0;
    }
}
