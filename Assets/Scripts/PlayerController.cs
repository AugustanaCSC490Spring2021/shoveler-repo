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
    private bool canMelee = true;
    private float meleeTimer = 0;
    private bool canShoot = false;

    public GameObject bullet;
    public GameObject projectileSpawn;

    private Camera camera;
    private Rigidbody rb;
    private SphereCollider meleeCollider;
    private Animator animator;


    private void Awake()
    {
        playerControls = new PlayerControls();

        // Binds fire function to Fire action
        playerControls.Land.Fire.performed += ctx => Fire();

        // Grab component from Player
        camera = this.GetComponentInChildren<Camera>();
        rb = this.GetComponent<Rigidbody>();
        meleeCollider = this.GetComponent<SphereCollider>();

        // Detact camera so it is not influenced by player rotation
        camera.transform.SetParent(null);

        animator = this.GetComponentInChildren<Animator>();
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

        // Move player to desired position
        this.transform.position = currentPos;

        // Update currentPos to reposition camera as well
        currentPos.y += 9;
        currentPos.z += -9;
        camera.transform.position = currentPos;

        // Reset for next loop
        xinput = 0;
        zinput = 0;

        // Player rotation
        this.transform.LookAt(GetPlayerTarget().point);

        // Disable melee after 100 milliseconds
        if (meleeTimer < Time.time * 1000) meleeCollider.enabled = false;
    }

    void Fire()
    {
        if (timeBetweenFire < Time.time)
        {
            timeBetweenFire = Time.time + (200 - fireRate) / 1000;
            if (canMelee)
            {
                Debug.Log("Swing!");
                animator.Play("Attack");
                meleeTimer = (Time.time * 1000) + 100;
                meleeCollider.enabled = true;
            }

            // Legacy code. Might reuse later
            if (canShoot)
            {
                RaycastHit hit = GetPlayerTarget();

                GameObject bulletInstance = Instantiate(bullet, projectileSpawn.transform.position, projectileSpawn.transform.rotation);

                Bullet bulletScript = bulletInstance.GetComponent<Bullet>();
                bulletScript.collideWithPlayer = false;

                Rigidbody bulletRB = bulletInstance.GetComponent<Rigidbody>();

                Vector3 shootDirection = (hit.point - this.transform.position);
                bulletRB.velocity = shootDirection.normalized * 25;
            }
        }
    }

    RaycastHit GetPlayerTarget()
    {
        Vector2 firingPosition = playerControls.Land.FiringPosition.ReadValue<Vector2>();
        RaycastHit hit;
        Ray ray = camera.ScreenPointToRay(firingPosition);

        int layerMask1 = 1 << 8;
        int layerMask2 = 1 << 9;

        layerMask1 = layerMask1;
        layerMask2 = layerMask2;

        int finalMask = layerMask1 | layerMask2;
        finalMask = ~finalMask;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, finalMask))
        {
            hit.point = new Vector3(hit.point.x,
                                    this.transform.position.y,
                                    hit.point.z
                                    );
            return hit;
        }

        return new RaycastHit();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<Health>().Damage(20);
        }
    }
}
