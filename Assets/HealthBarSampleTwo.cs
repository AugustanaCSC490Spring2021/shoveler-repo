using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarSampleTwo : MonoBehaviour
{
    private GameObject playerObject;
    private Health playerHealth;

    public Slider slider;
    public Gradient gradient;
    public Image Fill;

    private void Awake(){
        playerObject = GameObject.FindGameObjectWithTag("Player");
        playerHealth = playerObject.GetComponent<Health>();
    }

    private void FixedUpdate()
    {
        slider.value = playerHealth.GetHealth();
        Fill.color = gradient.Evaluate(slider.normalizedValue);
    }
    public void setMaxHealth(int maxHealth){
        slider.maxValue = maxHealth;
        slider.value = maxHealth;
        Fill.color = gradient.Evaluate(1f);
    }

}
