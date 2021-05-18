using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSpeedPack : MonoBehaviour
{

    private GameObject playerObject;
    private PlayerController playerMovement;
    private Health playerHealth;
    private int count = 0;
    public int howManyTouch = 4;

// Healing
    public float healingPerColide = 20;
    private float maxHealth =100;

//speed
    public float speedBoostAmount;

    //Shield
    public float shieldPerTouch = 100;

    private void OnTriggerEnter(Collider collision)
    {
        Healing(collision);
        speedBoost(collision);
        ShielPack(collision);
    }

    private void OnTriggerExit(Collider collision) 
    {
         count += 1;
        //Debug.Log(count);
        if (collision.gameObject.tag == "Player" && count == howManyTouch) 
        {
            Destroy(this.gameObject); // Makes the health pack disappear.
        }
    }

    // Handls the health pack
    private void Healing( Collider collision) 
    {
        //Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.tag == "Player")
        {
            playerHealth = collision.gameObject.GetComponent<Health>();
            playerHealth.SetHealth((int)Mathf.Min(healingPerColide + playerHealth.GetHealth(), (maxHealth)));

        }
    }

    private void speedBoost(Collider collision) 
    {
        //Debug.Log(collision.gameObject.GetComponent<PlayerController>().playerSpeed);
        if (collision.gameObject.tag == "Player") 
        {
            playerMovement = collision.gameObject.GetComponent<PlayerController>();
            playerMovement.playerSpeed += speedBoostAmount;
        }
    }

    private void ShielPack(Collider collision)
    {
        //Debug.Log(collision.gameObject.GetComponent<PlayerController>().playerSpeed);
        if (collision.gameObject.tag == "Player")
        {
            playerHealth = collision.gameObject.GetComponent<Health>();
            playerHealth.setShield((int)Mathf.Min(shieldPerTouch + playerHealth.GetShield(), (playerHealth.GetMaxShield())));
        }
    }
}
