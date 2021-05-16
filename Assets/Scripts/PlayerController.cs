using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
    private bool canMove = true;

    public GameObject bullet;
    public GameObject projectileSpawn;
    public TMP_Text roomCodeDisplay;

    private Camera camera;
    private Rigidbody rb;
    private SphereCollider meleeCollider;
    private Animator animator;

    private GameObject doorCollider;
    private GameObject exitCollider;
    private DungeonManager dungeonManager;
    private ClientScript clientScript;

    private void Awake()
    {
        playerControls = new PlayerControls();

        // Binds fire function to Fire action
        playerControls.Land.Fire.performed += ctx => Fire();

        // Binds e key to inteacting with doors
        playerControls.Land.Interact.performed += ctx => Interact();

        // Grab component from Player
        camera = this.GetComponentInChildren<Camera>();
        rb = this.GetComponent<Rigidbody>();
        meleeCollider = this.GetComponent<SphereCollider>();

        // Detact camera so it is not influenced by player rotation
        camera.transform.SetParent(null);

        animator = this.GetComponentInChildren<Animator>();

        dungeonManager = GameObject.Find("DungeonManager").GetComponent<DungeonManager>();
    }

    private void Start()
    {
        if(GameObject.Find("MultiplayerManager") != null)
        {
            clientScript = GameObject.Find("MultiplayerManager").GetComponentInChildren<ClientScript>();

            string roomCode = PlayerPrefs.GetString("roomCode");
            if (roomCode != "")
            {
                roomCodeDisplay.text = "Give this to your partner: " + roomCode;
            }
        }else
        {
            PlayerPrefs.SetString("roomCode", "");
        }
        
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
        if (canMove)
        {
            this.transform.position = currentPos;
        }

        // Update currentPos to reposition camera as well
        currentPos.y += 18;
        currentPos.z -= 4;
        camera.transform.position = currentPos;

        // Reset for next loop
        xinput = 0;
        zinput = 0;

        // Player rotation
        this.transform.LookAt(GetPlayerTarget().point);

        // Disable melee after 100 milliseconds
        if (meleeTimer < Time.time * 1000) meleeCollider.enabled = false;

        string roomCode = PlayerPrefs.GetString("roomCode");
        if (roomCode == "")
        {
            roomCodeDisplay.text = "";
        }

        if(clientScript.finished)
        {
            Debug.Log("Results:");
            Debug.Log(clientScript.enemyTime);
            Debug.Log(clientScript.enemyScore);
        }
    }

    void Fire()
    {
        if (timeBetweenFire < Time.time)
        {
            timeBetweenFire = Time.time + (200 - fireRate) / 1000;
            if (canMelee)
            {
                //Debug.Log("Swing!");
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

        int layerMask1 = 1 << 2;
        int layerMask2 = 1 << 8;
        int layerMask3 = 1 << 9;

        //layerMask1 = layerMask1;
        //layerMask2 = layerMask2;

        int finalMask = layerMask1 | layerMask2 | layerMask3;
        finalMask = ~finalMask;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, finalMask))
        {
            //Debug.Log(hit.collider.);
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
        switch (collision.gameObject.tag)
        {
            case "Enemy":
                collision.gameObject.GetComponent<Health>().Damage(20);
                break;
            case "Door":
                doorCollider = collision.gameObject;
                break;
            case "Exit":
                exitCollider = collision.gameObject;
                break;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        switch(collision.gameObject.tag)
        {
            case "Door":
                doorCollider = null;
                break;
            case "Exit":
                exitCollider = null;
                break;
        }
    }

    public void setMove(bool shouldMove)
    {
        if(shouldMove)
        {
            rb.useGravity = true;
            canMove = true;
        }else
        {
            rb.useGravity = false;
            canMove = false;
        }
    }

    void Interact()
    {
        if (doorCollider != null && doorCollider.GetComponentInParent<RoomManager>().getRoomCleared())
        {
            doorCollider.GetComponent<DoorColliderManager>().movePlayer(gameObject);
        }

        if(exitCollider != null)
        {
            Timer timer = this.GetComponentInChildren<Timer>();
            clientScript.SendScore( (long) Time.time, 1234);
        }
    }




}
