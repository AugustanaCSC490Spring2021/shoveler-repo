using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float xinput, zinput = 0;
    public float playerSpeed = 0.01f;

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

    // Update is called once per frame
    void Update()
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

        zinput *= playerSpeed;
        xinput *= playerSpeed;

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

        // Player weapon fire
        if(Input.GetMouseButtonDown(0))
        {
            Rigidbody newrb = Instantiate(bullet, projectileSpawn.transform.position, projectileSpawn.transform.rotation).GetComponent<Rigidbody>();
            Vector3 shootDirection = (hit.point - this.transform.position);
            newrb.velocity = shootDirection.normalized * 25;
        }
    }
}
