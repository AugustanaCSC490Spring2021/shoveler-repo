using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float bulletLife = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        bulletLife = Time.time + 3f;
    }

    void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(bulletLife - Time.time);
        if (bulletLife <= Time.time) Destroy(gameObject);
    }
}
