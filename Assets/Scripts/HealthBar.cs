using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private GameObject playerObject;
    private Health playerHealth;

    public Slider slider;
    public Gradient gradient;
    public Image Fill;
    public Text HealthText;

    private void Awake()
    {
        playerObject = GameObject.FindGameObjectWithTag("Player");
        playerHealth = playerObject.GetComponent<Health>();
        // Landen is settings this for temp testing. We can change later if we need.
        SetMaxHealth(100);
    }

    private void FixedUpdate()
    {
        HealthText.text = playerHealth.GetHealth().ToString();
        slider.value = playerHealth.GetHealth();
        Fill.color = gradient.Evaluate(slider.normalizedValue);
        Debug.Log(playerHealth.GetHealth());
    }
    public void SetMaxHealth(int maxHealth)
    {
        slider.maxValue = maxHealth;
        slider.value = maxHealth;
        Fill.color = gradient.Evaluate(1f);
    }

}
