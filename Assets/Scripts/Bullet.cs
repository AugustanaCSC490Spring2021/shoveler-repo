using System.Collections;
using System.Collections.Generic;
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
        if(collideWithPlayer)
        {
            Destroy(gameObject);
        }else
        {
            if(collision.collider.tag != "Player") Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (bulletLife <= Time.time) Destroy(gameObject);
    }
}
