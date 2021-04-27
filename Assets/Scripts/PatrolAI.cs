//this website provided this code and contains instructions for
//setting points for patrol
//https://docs.unity3d.com/Manual/nav-AgentPatrol.html

using UnityEngine;
using UnityEngine.AI;
using System.Collections;


public class PatrolAI : MonoBehaviour
{

    [SerializeField] private GameObject playerObj = null;
    [SerializeField] private Vector3 playerPos;
    [SerializeField] private float radius;
    [SerializeField] private float speed;
    [SerializeField] public Transform[] points;
    [SerializeField] private int destPoint = 0;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] public bool chasingPlayer;
    [SerializeField] public bool wasChasingPlayer;


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        // Disabling auto-braking allows for continuous movement
        // between points (ie, the agent doesn't slow down as it
        // approaches a destination point).
        agent.autoBraking = false;

        GotoNextPoint();
        wasChasingPlayer = false;
    }


    void GotoNextPoint()
    {
        // Returns if no points have been set up
        if (points.Length == 0)
            return;

        // Set the agent to go to the currently selected destination.
        agent.destination = points[destPoint].position;

        // Choose the next point in the array as the destination,
        // cycling to the start if necessary.
        destPoint = (destPoint + 1) % points.Length;
    }


    void Update()
    {
        playerPos = playerObj.transform.position;

        if (transform.position.x - playerPos.x < radius &&
            transform.position.y - playerPos.y < radius &&
            transform.position.z - playerPos.z < radius)
        {
            chasingPlayer = true;
        }

        // Choose the next destination point when the agent gets
        // close to the current one.
        if (!agent.pathPending && agent.remainingDistance < 0.5f && !chasingPlayer)
        {
            GotoNextPoint();
        }

        if (chasingPlayer && !wasChasingPlayer)
        {
            agent.ResetPath();
            wasChasingPlayer = true;
        }
           

        if (chasingPlayer)
        {
            transform.position = Vector3.MoveTowards(transform.position, playerPos, Time.deltaTime * speed);
        }
    }
}