using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tempo : MonoBehaviour
{

    private GameObject playerObject;
    private Health playerHealth;
    private int count = 0;
    public int howManyHealing = 4;
    public float healingPerColide;
    public float maxHealth =100;

    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.tag == "Player") {
            playerHealth = collision.gameObject.GetComponent<Health>();
            playerHealth.AddHealth((int)Mathf.Min(healingPerColide + playerHealth.GetHealth(), (maxHealth)));

        }    
    }

    private void OnTriggerExit(Collider collision) {
         count += 1;
        Debug.Log(count);
        if (collision.gameObject.tag == "Player" && count == howManyHealing) {
            Destroy(this.gameObject); // Makes the health pack disappear.
        }
    }
}
