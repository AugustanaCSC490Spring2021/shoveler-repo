using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    //set variable of which direction this spawnpoint will need a door facing
    //1 is down, 2 is up, 3 is left, 4 is right
    public int openingDirection;
    private RoomTemplates templates;
    private int rand;
    private bool spawned = false;


    private void Start()
    {
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        Invoke("Spawn", .1f);
    }



    private void Spawn()
    {
        if (spawned == false)
        {
            if (openingDirection == 1)
            {
                //spawn room with bottom door
                rand = Random.Range(0, templates.downRooms.Length);
                Instantiate(templates.downRooms[rand], transform.position, templates.downRooms[rand].transform.rotation);
            }
            else if (openingDirection == 2)
            {
                //spawn room with upwards door
                rand = Random.Range(0, templates.upRooms.Length);
                Instantiate(templates.upRooms[rand], transform.position, templates.upRooms[rand].transform.rotation);
            }
            else if (openingDirection == 3)
            {
                // spawn room with left door
                rand = Random.Range(0, templates.leftRooms.Length);
                Instantiate(templates.leftRooms[rand], transform.position, templates.leftRooms[rand].transform.rotation);
            }
            else if (openingDirection == 4)
            {
                // spawn room with right door
                rand = Random.Range(0, templates.rightRooms.Length);
                Instantiate(templates.rightRooms[rand], transform.position, templates.rightRooms[rand].transform.rotation);
            }
            spawned = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("RoomCollider"))
        {
            Destroy(gameObject);
        }
    }




}
