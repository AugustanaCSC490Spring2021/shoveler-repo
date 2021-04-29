using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerControls playerControls;

    private float xinput, zinput = 0;
    public float playerSpeed = 100;
    public float fireRate = 5;
    private float timeBetweenFire;

    public GameObject LookDirection;
    public GameObject bullet;
    public GameObject projectileSpawn;

    private Camera camera;
    private Rigidbody rb;


    private void Awake()
    {
        playerControls = new PlayerControls();

        // Binds fire function to Fire action
        playerControls.Land.Fire.performed += ctx => Fire();

        // Grab component from Player
        camera = this.GetComponentInChildren<Camera>();
        rb = this.GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    private void FixedUpdate()
    {
        // Grab player input and transform player
        float zinput = playerControls.Land.MoveForward.ReadValue<float>();
        float xinput = playerControls.Land.MoveRight.ReadValue<float>();
        Vector2 firingPosition = playerControls.Land.FiringPosition.ReadValue<Vector2>();

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
        LookDirection.transform.LookAt(GetPlayerTarget().point);
    }

    void Fire()
    {
        if (timeBetweenFire < Time.time)
        {
            RaycastHit hit = GetPlayerTarget();

            // Limits fire rate
            timeBetweenFire = Time.time + (200 - fireRate) / 1000;
            GameObject bulletInstance = Instantiate(bullet, projectileSpawn.transform.position, projectileSpawn.transform.rotation);

            Bullet bulletScript = bulletInstance.GetComponent<Bullet>();
            bulletScript.collideWithPlayer = false;

            Rigidbody bulletRB = bulletInstance.GetComponent<Rigidbody>();

            Vector3 shootDirection = (hit.point - this.transform.position);
            bulletRB.velocity = shootDirection.normalized * 25;
        }
    }

    RaycastHit GetPlayerTarget()
    {
        Vector2 firingPosition = playerControls.Land.FiringPosition.ReadValue<Vector2>();
        RaycastHit hit;
        Ray ray = camera.ScreenPointToRay(firingPosition);

        int layerMask = 1 << 8;

        layerMask = ~layerMask;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
        {
            hit.point = new Vector3(hit.point.x,
                                    this.transform.position.y,
                                    hit.point.z
                                    );
            return hit;
        }

        return new RaycastHit();
    }
}
