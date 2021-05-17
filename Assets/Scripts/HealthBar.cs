using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private GameObject playerObject;
    private Health playerHealth;
    private PlayerController playerController;

    public Slider slider;
    public Gradient gradient;
    public Image Fill;
    public Text HealthText;
    public Text ScoreText;

    private void Awake()
    {
        playerObject = GameObject.FindGameObjectWithTag("Player");
        playerHealth = playerObject.GetComponent<Health>();
        playerController = playerObject.GetComponent<PlayerController>();
        // Landen is settings this for temp testing. We can change later if we need.
        SetMaxHealth(playerHealth.GetHealth(), playerController.score);
    }

    private void FixedUpdate()
    {
        ScoreText.text = playerController.score.ToString();
        HealthText.text = playerHealth.GetHealth().ToString();
        slider.value = playerHealth.GetHealth();
        Fill.color = gradient.Evaluate(slider.normalizedValue);
        //Debug.Log(playerHealth.GetHealth());
    }
    public void SetMaxHealth(int maxHealth, long score)
    {
        slider.maxValue = maxHealth;
        slider.value = maxHealth;
        Fill.color = gradient.Evaluate(1f);
    }

}
