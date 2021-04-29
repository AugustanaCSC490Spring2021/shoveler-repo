using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float xinput, zinput = 0;
    public float playerSpeed = 100;
    public float fireRate = 5;
    private float timeBetweenFire;

    public GameObject LookDirection;
    public GameObject bullet;
    public GameObject projectileSpawn;

    private Camera camera;
    private Rigidbody rb;
    

    // Start is called before the first frame update
    void Start()
    {
        camera = this.GetComponentInChildren<Camera>();
        rb = this.GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        // WASD controls for player
        if (Input.GetKey("w"))
        {
            zinput++;
        }
        if (Input.GetKey("s"))
        {
            zinput--;
        }
        if (Input.GetKey("a"))
        {
            xinput--;
        }
        if (Input.GetKey("d"))
        {
            xinput++;
        }

        zinput *= (playerSpeed / 1000);
        xinput *= (playerSpeed / 1000);

        Vector3 currentPos = this.transform.position;
        currentPos.z += zinput;
        currentPos.x += xinput;

        this.transform.position = currentPos;

        // Reset for next loop
        xinput = 0;
        zinput = 0;

        // Player rotation
        RaycastHit hit;
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            hit.point = new Vector3(hit.point.x,
                                    this.transform.position.y,
                                    hit.point.z
                );
            LookDirection.transform.LookAt(hit.point);
        }

        //Debug.Log(timeBetweenFire - Time.time);
        // Player weapon fire
        if (Input.GetMouseButton(0) & timeBetweenFire < Time.time)
        {
            timeBetweenFire = Time.time + (200 - fireRate) / 1000;
            GameObject bulletInstance = Instantiate(bullet, projectileSpawn.transform.position, projectileSpawn.transform.rotation);

            Bullet bulletScript = bulletInstance.GetComponent<Bullet>();
            bulletScript.collideWithPlayer = false;

            Rigidbody bulletRB = bulletInstance.GetComponent<Rigidbody>();

            Vector3 shootDirection = (hit.point - this.transform.position);
            bulletRB.velocity = shootDirection.normalized * 25;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
