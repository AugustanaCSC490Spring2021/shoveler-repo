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

    [SerializeField] private Canvas messageCanvas;

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

    [SerializeField] private string[] covidFacts;
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

        covidFactGenerator();
    }

    void Update()
    {
        healthBar.fillAmount = (float)patrolHealth.GetHealth() / maxHealth;

        if (patrolHealth.GetHealth() <= 0)
        {
            //display covid factoid for period of time and then delete the enemy
            Canvas newMessage = Instantiate(messageCanvas);
            newMessage.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 4, this.transform.position.z);
            newMessage.transform.eulerAngles = new Vector3(30, 0, 0);
            newMessage.GetComponent<MessageManager>().SetEnemyType(2);

            int randomFact = Random.Range(0, 9);
            newMessage.GetComponentInChildren<Text>().text = covidFacts[randomFact];

            agent.SetDestination(new Vector3(0, -5, 0));
            this.transform.position = new Vector3(0, -5, 0);

            Destroy(gameObject);
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

    private void OnCollisionStay(Collision collision)
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

    void covidFactGenerator()
    {
        /*
         * Sources:
         * https://www.goodrx.com/blog/how-the-immune-system-fights-covid-19/
         * https://www.nebraskamed.com/COVID/what-the-coronavirus-does-to-your-body#:~:text=As%20the%20body%20tries%20to,can%20lead%20to%20pneumonia.
         */

        covidFacts = new string[9];

        covidFacts[0] = "As your body tries to fight an infection, your immune " +
                        "system can cause inflamation which leads to your symptoms.";
        covidFacts[1] = "Viruses spread through your body by making copies " +
                        "of themselves.";
        covidFacts[2] = "The immune system is comprised of 2 parts: " +
                        "The Innate Immune System and The Adaptive Immune System.";
        covidFacts[3] = "The Innate Immune System is your first line of defense " +
                        "against viruses. It provides a general defense against invaders.";
        covidFacts[4] = "The Adaptive Immune System develops antibodies and white blood " +
                        "cells (that's you!) to both fight and remember the virus.";
        covidFacts[5] = "White blood cells (that's you!) create antibodies which bind " +
                        "to the virus and aid in destroying it.";
        covidFacts[6] = "Some white blood cells (that's you!) are stored as memory of the " +
                        "virus so that the body can fight it better in the future.";
        covidFacts[7] = "Vaccines stimulate your body to create long lasting memory cells. " +
                        "These cells help your body fight off future infections much better.";
        covidFacts[8] = "Severe symptoms from COVID-19 are often the result of an out of sync " +
                        "immune system. Your body inflames to fight, but never destroys the virus.";
        covidFacts[9] = "There are ways to boost your immune system! For example: " +
                        "Getting proper sleep, a healthy diet, and exercise.";
    }

}