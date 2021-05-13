using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class GoombaAI : MonoBehaviour
{

    #region variables

    [SerializeField] private GameObject playerObj;
    [SerializeField] private NavMeshAgent agent;

    [SerializeField] private Vector3 playerPos;
    [SerializeField] private float speed;
    [SerializeField] private float acceleration;

    [SerializeField] private Health goombaHealth;
    [SerializeField] private int maxHealth;

    [SerializeField] private int playerDamage;
    [SerializeField] private Health playerHealth;

    [SerializeField] private float attackSpeedInSeconds;
    [SerializeField] private int goombaDamage;

    [SerializeField] private Image healthBar;

    [SerializeField] private float timeLastAttacked;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        //we can substitute "Player" for whatever we name our player character
        playerObj = GameObject.FindGameObjectWithTag("Player");
        playerHealth = playerObj.GetComponent<Health>();

        agent = GetComponent<NavMeshAgent>();

        playerPos = playerObj.transform.position;

        goombaHealth = this.GetComponent<Health>();
        maxHealth = goombaHealth.GetHealth();

        agent.speed = speed;
        agent.acceleration = acceleration;
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.fillAmount = (float)goombaHealth.GetHealth() / maxHealth;

        if (goombaHealth.GetHealth() <= 0)
        {
            //temporary line to simply delete the enemy when it is killed.
            Destroy(gameObject);

            //to-do add some way to broadcast the death of this enemy so that
            //we might open the doors upon there being no enemies left

        }

        playerPos = playerObj.transform.position;

        //moves this object towards the players position
        //transform.position = Vector3.MoveTowards(transform.position, playerPos, Time.deltaTime * speed);
        agent.SetDestination(playerPos);

        //this will hard lock the rotation of the whole sprite
        //this.transform.rotation = new Quaternion(0f, 0f, 0f, 0f);
    }


    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Player") && (Time.time - timeLastAttacked > attackSpeedInSeconds))
        {

            playerHealth.Damage(goombaDamage);
            //Debug.Log("Goomba Damaging Player!");

            //testing purposes
            //takeDamage(20);

            timeLastAttacked = Time.time;
        }

    }

    /*
     * this was before the player handled damaging enemies
    public void takeDamage(int damage)
    {
        goombaHealth.Damage(damage);
        healthBar.fillAmount = (float)goombaHealth.GetHealth() / maxHealth;
        //Debug.Log(goombaHealth.GetHealth() + " / " + maxHealth);
    }
    */
}
