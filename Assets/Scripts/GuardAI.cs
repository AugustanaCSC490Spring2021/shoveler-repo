using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class GuardAI : MonoBehaviour
{

    #region variables

    [SerializeField] private GameObject playerObj;
    [SerializeField] private Canvas messageCanvas;
    [SerializeField] private RoomManager myRoomManager;

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

    [SerializeField] private string[] covidFacts;
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

        covidFactGenerator();
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.fillAmount = (float)guardHealth.GetHealth() / maxHealth;

        if (guardHealth.GetHealth() <= 0)
        {
            myRoomManager.removeDeadEnemy(gameObject);
            playerObj.GetComponent<PlayerController>().score += 8;

            //display covid factoid for period of time and then delete the enemy
            Canvas newMessage = Instantiate(messageCanvas);
            newMessage.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 4, this.transform.position.z);
            newMessage.transform.eulerAngles = new Vector3(90, 0, 0);
            newMessage.GetComponent<MessageManager>().SetEnemyType(1);

            int randomFact = Random.Range(0, 9);
            newMessage.GetComponentInChildren<Text>().text = covidFacts[randomFact];

            this.transform.position = new Vector3(0, -5, 0);

            Destroy(gameObject);

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

    private void OnCollisionStay(Collision collision)
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

    void covidFactGenerator()
    {

        /*
         * Sources:
         * https://www.cdc.gov/coronavirus/2019-ncov/faq.html
         * https://www.cdc.gov/coronavirus/2019-ncov/symptoms-testing/symptoms.html
         * https://www.cdc.gov/coronavirus/2019-ncov/prevent-getting-sick/prevention.html
         */

        covidFacts = new string[10];

        covidFacts[0] = "To slow the spread of COVID-19, you should wear " +
                        "a mask if you are around other people.";
        covidFacts[1] = "Even when wearing a mask, you should continue to " +
                        "socially distance (at least 6ft!).";
        covidFacts[2] = "There are several vaccines available for COVID-19. " +
                        "Getting the vaccine will help you fight off the virus!";
        covidFacts[3] = "It is good to wash your hands with soap and water as " +
                        "often as possible. Bring hand sanitizer when going out!";
        covidFacts[4] = "If you must be around others, it is better to be " +
                        "outside, and in well ventilated spaces.";
        covidFacts[5] = "If you have to cough or sneeze, don't lower you mask! " +
                        "Just change into a clean mask as soon as possible.";
        covidFacts[6] = "You should clean and disinfect high touch surfaces daily. " +
                        "This includes doorknobs, phones, keyboards, and many others.";
        covidFacts[7] = "Be on the lookout for symptoms! Don't brush potential " +
                        "symptoms aside, and take your tempeartue if you feel unwell.";
        covidFacts[8] = "If you test positive for COVID-19, you should self " +
                        "quarantine as soon as possible.";
        covidFacts[9] = "If you have been exposed to COVID-19, you should self " +
                        "quarantine for 14 days, and be on the lookout for symptoms.";
    }

    #region setters and getters

    public void setRoomManager(RoomManager roomManager)
    {
        myRoomManager = roomManager;
    }

    public float getGuardSpeed()
    {
        return speed;
    }

    public void setGuardSpeed(float newSpeed)
    {
        speed = newSpeed;
    }

    public float getGuardAccel()
    {
        return acceleration;
    }

    public void setGuardAccel(float newAccel)
    {
        acceleration = newAccel;
    }

    public int getGuardMaxHealth()
    {
        return maxHealth;
    }

    public void setGuardMaxHealth(int newMax)
    {
        maxHealth = newMax;
    }

    public float getAttackSpeedInSeconds()
    {
        return attackSpeedInSeconds;
    }

    public void setAttackSpeedInSeconds(float newAttackSpeed)
    {
        attackSpeedInSeconds = newAttackSpeed;
    }

    public int getGuardDamage()
    {
        return guardDamage;
    }

    public void setGuardDamage(int newDamage)
    {
        guardDamage = newDamage;
    }

    #endregion

}
