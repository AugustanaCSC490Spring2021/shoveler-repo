using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class GuardAI : MonoBehaviour
{

    #region variables

    [SerializeField] private GameObject playerObj;
    [SerializeField] private Vector3 playerPos;
    [SerializeField] private float speed;
    [SerializeField] private float acceleration;

    //once the enemy starts chasing the player, it will not stop
    [SerializeField] private bool chasingPlayer = false;
    //How far away the player must be in order to get the attention of this enemy
    //not sure what size to make this number
    [SerializeField] private float radius;
    [SerializeField] private NavMeshAgent agent;

    [SerializeField] private Health guardHealth;
    [SerializeField] private int maxHealth;
    [SerializeField] private Health playerHealth;

    [SerializeField] private int playerDamage;
    [SerializeField] private int guardDamage;

    [SerializeField] private Image healthBar;

    [SerializeField] private float attackSpeedInSeconds;
    [SerializeField] private float timeLastAttacked;

    #endregion


    // Start is called before the first frame update
    void Start()
    {

        agent = GetComponent<NavMeshAgent>();

        //we can substitute "Player" for whatever we name our player character
        playerObj = GameObject.Find("Player");
        playerHealth = playerObj.GetComponent<Health>();

        playerPos = playerObj.transform.position;
        guardHealth = this.GetComponent<Health>();
        maxHealth = guardHealth.GetHealth();

        agent.speed = speed;
        agent.acceleration = acceleration;
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.fillAmount = (float)guardHealth.GetHealth() / maxHealth;

        if (guardHealth.GetHealth() <= 0)
        {
            //temporary line to simply delete the enemy when it is killed.
            Destroy(gameObject);

            //to-do add some way to broadcast the death of this enemy so that
            //we might open the doors upon there being no enemies left

        }

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
            //transform.position = Vector3.MoveTowards(transform.position, playerPos, Time.deltaTime * speed);
            agent.SetDestination(playerPos);
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        //need to account for when the player is not attacking
        //if (playerObj.isAttacking()) { health.Damage(playerDamage); }

        if (collision.gameObject.CompareTag("Player") && (Time.time - timeLastAttacked > attackSpeedInSeconds))
        {

            playerHealth.Damage(guardDamage);
            //Debug.Log("Guard Damaging Player!");

            //testing purposes
            //takeDamage(20);

            timeLastAttacked = Time.time;
        }
    }

    /*
     * this was before the player handled damaging enemies
    public void takeDamage(int damage)
    {
        guardHealth.Damage(damage);
        healthBar.fillAmount = (float)guardHealth.GetHealth() / maxHealth;
        //Debug.Log(guardHealth.GetHealth() + " / " + maxHealth);
    }
    */
}
