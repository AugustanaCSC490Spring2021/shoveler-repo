using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GuardAI : MonoBehaviour
{

    #region variables

    [SerializeField] private GameObject playerObj = null;
    [SerializeField] private Vector3 playerPos;
    [SerializeField] private float speed = 1;
    //once the enemy starts chasing the player, it will not stop
    [SerializeField] private bool chasingPlayer = false;
    //How far away the player must be in order to get the attention of this enemy
    //not sure what size to make this number
    [SerializeField] private float radius = 100;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private int health = 8;

    #endregion


    // Start is called before the first frame update
    void Start()
    {

        agent = GetComponent<NavMeshAgent>();

        if (playerObj == null)
        {
            //we can substitute "Player" for whatever we name our player character
            playerObj = GameObject.Find("Player");
        }

    }

    // Update is called once per frame
    void Update()
    {
        //gets the players current position
        playerPos = playerObj.transform.position;

        //in theory this code creates a bubble around the enemy, and once the player enters
        //the bubble the enemy will begin to chase
        if (transform.position.x - playerPos.x < radius &&
            transform.position.y - playerPos.y < radius &&
            transform.position.z - playerPos.z < radius)
        {
            chasingPlayer = true;
        }
        
        if (chasingPlayer)
        {
            //moves this object towards the players position
            agent.Move(Vector3.MoveTowards(transform.position, playerPos, Time.deltaTime * speed));
        }

    }
}
