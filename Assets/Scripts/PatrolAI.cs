//this website provided this code and contains instructions for
//setting points for patrol
//https://docs.unity3d.com/Manual/nav-AgentPatrol.html

using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using UnityEngine.UI;


public class PatrolAI : MonoBehaviour
{

    #region variables

    [SerializeField] private GameObject playerObj;
    [SerializeField] private Vector3 playerPos;

    [SerializeField] private float radius;
    [SerializeField] private float speed;
    [SerializeField] private float acceleration;

    [SerializeField] public Transform[] points;
    [SerializeField] private int destPoint = 0;
    [SerializeField] private NavMeshAgent agent;

    [SerializeField] public bool chasingPlayer;

    [SerializeField] private Health patrolHealth;
    [SerializeField] private int maxHealth;

    [SerializeField] private float waitTime;
    [SerializeField] private float elapsedTime;

    [SerializeField] private Vector3 currentPointPosition;
    [SerializeField] private int currentPointIndex;
    [SerializeField] private bool hasStopped;

    [SerializeField] private Health playerHealth;
    [SerializeField] private int patrolDamage;
    [SerializeField] private float attackSpeedInSeconds;
    [SerializeField] private Image healthBar;

    [SerializeField] private float timeLastAttacked;

    #endregion

    void Start()
    {
        playerObj = GameObject.FindGameObjectWithTag("Player");
        playerHealth = playerObj.GetComponent<Health>();
        agent = GetComponent<NavMeshAgent>();

        playerPos = playerObj.transform.position;
        patrolHealth = this.GetComponent<Health>();
        maxHealth = patrolHealth.GetHealth();

        //sets our current point to be the first in the array
        currentPointPosition = points[0].position;
        currentPointIndex = 0;

        hasStopped = false;
        agent.speed = speed;
        agent.acceleration = acceleration;
    }

    void Update()
    {
        healthBar.fillAmount = (float)patrolHealth.GetHealth() / maxHealth;

        if (patrolHealth.GetHealth() <= 0)
        {
            //temporary line to simply delete the enemy when it is killed.
            Destroy(gameObject);

            //to-do add some way to broadcast the death of this enemy so that
            //we might open the doors upon there being no enemies left

        }

        playerPos = playerObj.transform.position;


        chasingPlayer = inRadius();
        

        if (chasingPlayer)
        {
            //moves this object towards the players position
            //transform.position = Vector3.MoveTowards(transform.position, playerPos, Time.deltaTime * speed);
            agent.SetDestination(playerPos);
        } else
        {

            //checks if we have reached the current patrol point
             if (reachedPoint() && !hasStopped)
             {

                changeCurrentPoint();

                //transform.position = Vector3.MoveTowards(transform.position, currentPointPosition, Time.deltaTime * speed);
                agent.SetDestination(currentPointPosition);

                hasStopped = true;
             } else
             {
                //moves us towards the next patrol point
                //transform.position = Vector3.MoveTowards(transform.position, currentPointPosition, Time.deltaTime * speed);
                agent.SetDestination(currentPointPosition);

                hasStopped = false;
             }
            
        }

    }

    bool inRadius()
    {
        return Mathf.Abs(transform.position.x - playerPos.x) < radius &&
               Mathf.Abs(transform.position.y - playerPos.y) < radius &&
               Mathf.Abs(transform.position.z - playerPos.z) < radius;
    }


    void changeCurrentPoint()
    {

        Debug.Log(currentPointIndex + " Index " + points.Length + " Length");
        //make sure we have not gone beyond the final point
        if (currentPointIndex < points.Length)
        {
            //move our focus to the next point
            currentPointIndex++;

        } else
        {
            //reset our index
            currentPointIndex = 0;
        }

        //change our current point to be the next in line
        currentPointPosition = points[currentPointIndex].position;
    }


    bool reachedPoint()
    {
        return Mathf.Abs(transform.position.x - currentPointPosition.x) < .1 &&
                Mathf.Abs(transform.position.z - currentPointPosition.z) < .1;
    }

    private void OnCollisionEnter(Collision collision)
    {
        //need to account for when the player is not attacking
        //if (playerObj.isAttacking()) { health.Damage(playerDamage); }

        if (collision.gameObject.CompareTag("Player") && (Time.time - timeLastAttacked > attackSpeedInSeconds))
        {
            playerHealth.Damage(patrolDamage);
            //Debug.Log("Patrol Damaging Player!");

            //testing purposes
            //takeDamage(20);

            timeLastAttacked = Time.time;
        }
    }

    public void setPoints (Transform point1, Transform point2)
    {
        points[1] = point1;
        points[2] = point2;
    }

    /*
     * this was before the player handled damaging enemies
    public void takeDamage(int damage)
    {
        patrolHealth.Damage(damage);
        healthBar.fillAmount = (float)patrolHealth.GetHealth() / maxHealth;
        Debug.Log(patrolHealth.GetHealth() + " / " + maxHealth);
    }
    */
}