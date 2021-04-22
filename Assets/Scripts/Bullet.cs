using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float bulletLife = 0.0f;
    public bool collideWithPlayer = true;

    // Start is called before the first frame update
    void Start()
    {
        bulletLife = Time.time + 3f;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag != "Bullet")
        {
            if(collision.collider.tag == "Enemy")
            {
                try
                {
                    PlayerHealth enemyHealth = collision.gameObject.GetComponent<PlayerHealth>();
                    enemyHealth.Damage(20);
                }catch(Exception e)
                {
                    //nothing
                }
            }

            if (collideWithPlayer)
            {
                Destroy(gameObject);
            }
            else
            {
                if (collision.collider.tag != "Player") Destroy(gameObject);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (bulletLife <= Time.time) Destroy(gameObject);
    }
}
