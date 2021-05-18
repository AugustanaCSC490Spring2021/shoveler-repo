using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class shildBar : MonoBehaviour
{
    private GameObject playerObject;
    private Health playerHealth;

    public Slider slider;
    public Text ShieldText;

    private void Awake()
    {
        playerObject = GameObject.FindGameObjectWithTag("Player");
        playerHealth = playerObject.GetComponent<Health>();
               // Landen is settings this for temp testing. We can change later if we need.
        SetMaxHealth(playerHealth.shield, playerHealth.GetMaxShield());
    }

    private void FixedUpdate()
    {
        ShieldText.text = playerHealth.GetShield().ToString();
        slider.value = playerHealth.GetShield();
                Debug.Log(playerHealth.GetHealth());
    }
    public void SetMaxHealth(int maxShield, int shield)
    {
        slider.maxValue = maxShield;
        slider.value = shield;
      
    }
}
