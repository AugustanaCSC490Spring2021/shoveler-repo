using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSpeedPack : MonoBehaviour
{

    private GameObject playerObject;
    private PlayerController playerMovement;
    private Health playerHealth;
    private int count = 0;
    public int howManyHealing = 4;
    public float healingPerColide = 20;
    public float maxHealth =100;
    public float speedBoostAmount;

    private void OnTriggerEnter(Collider collision)
    {
        Healing(collision);
        speedBoost(collision);
    }

    private void OnTriggerExit(Collider collision) {
         count += 1;
        //Debug.Log(count);
        if (collision.gameObject.tag == "Player" && count == howManyHealing) {
            Destroy(this.gameObject); // Makes the health pack disappear.
        }
    }

    // Handls the health pack
    private void Healing( Collider collision) {
        //Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.tag == "Player")
        {
            playerHealth = collision.gameObject.GetComponent<Health>();
            playerHealth.SetHealth((int)Mathf.Min(healingPerColide + playerHealth.GetHealth(), (maxHealth)));

        }
    }

    private void speedBoost(Collider collision) {
        //Debug.Log(collision.gameObject.GetComponent<PlayerController>().playerSpeed);
        if (collision.gameObject.tag == "Player") {
            playerMovement = collision.gameObject.GetComponent<PlayerController>();
            playerMovement.playerSpeed += speedBoostAmount;
        }
    }
}
