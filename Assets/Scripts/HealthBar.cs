﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Gradient gradient;
    public Slider slider;
    private Image healthBarFI;
    int health = 100;

    private GameObject playerObject;
    private Health playerHealthScript;

    //playerObject = GameObject.FindGameObjectWithTag("Player");
    //playerHealthScript = playerObject.GetComponent<Health>();


    
    // Start is called before the first frame update
    public void SetMaxHealth(int health) {
        slider.maxValue = health;
        slider.value = health;
        healthBarFI.color = gradient.Evaluate(1f);
    }
    public void setHealth(int health) {
        slider.value = health;
        healthBarFI.color = gradient.Evaluate(slider.normalizedValue);
    }
}