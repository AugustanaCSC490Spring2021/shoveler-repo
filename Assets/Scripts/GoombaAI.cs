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
    [SerializeField] private Canvas messageCanvas;
    [SerializeField] private RoomManager myRoomManager;

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
    [SerializeField] private string[] covidFacts;


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

        covidFactGenerator();
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.fillAmount = (float)goombaHealth.GetHealth() / maxHealth;

        if (goombaHealth.GetHealth() <= 0)
        {
            myRoomManager.removeDeadEnemy(gameObject);

            //display covid factoid for period of time and then delete the enemy
            Canvas newMessage = Instantiate(messageCanvas);
            newMessage.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 4, this.transform.position.z);
            newMessage.transform.eulerAngles = new Vector3(90,0,0);
            newMessage.GetComponent<MessageManager>().SetEnemyType(0);

            int randomFact = Random.Range(0, 9);
            newMessage.GetComponentInChildren<Text>().text = covidFacts[randomFact];

            agent.SetDestination(new Vector3(0, -5, 0));
            this.transform.position = new Vector3(0, -5, 0);

            Destroy(gameObject);

        }

        playerPos = playerObj.transform.position;

        //moves this object towards the players position
        //transform.position = Vector3.MoveTowards(transform.position, playerPos, Time.deltaTime * speed);
        agent.SetDestination(playerPos);

        //this will hard lock the rotation of the whole sprite
        //this.transform.rotation = new Quaternion(0f, 0f, 0f, 0f);
    }


    private void OnCollisionStay(Collision collision)
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

    private void covidFactGenerator()
    {

        /*
         * Sources:
         * https://www.worldometers.info/coronavirus/?utm_campaign=homeAdUOA?Si
         * https://www.goodrx.com/blog/flu-vs-coronavirus-mortality-and-death-rates-by-year/
         * https://www.cdc.gov/coronavirus/2019-ncov/symptoms-testing/symptoms.html
         */

        covidFacts = new string[9];

        covidFacts[0] = "COVID-19 spreads mainly through respiratory " +
                        "droplets produced from sneezes and coughs.";
        covidFacts[1] = "COVID-19 is much more likely to spread when " +
                        "people are within 6ft of one another.";
        covidFacts[2] = "COVID-19 spreads easily from person to person. " +
                        "More so than Influenza, but less so than measles.";
        covidFacts[3] = "Community spread can occur when several people " +
                        "in an area are infected, and some are not sure how!";
        covidFacts[4] = "COVID-19 spreads mainly from person to person. " +
                        "It does not travel through mosquitos and ticks.";
        covidFacts[5] = "There have been 160 million cases of COIVD-19 " +
                        "reported worldwide as of May, 2021.";
        covidFacts[6] = "COVID-19 has a mortality rate of roughly 3%, " +
                        "while Influenza(The Flu) sits at around 0.1%!";
        covidFacts[7] = "COVID-19 can cause you to temporarily lose " +
                        "your sense of smell!";
        covidFacts[8] = "Some common symptoms of COVID-19 are: " +
                        "fever, cough, shortness of breath, and headaches.";
        covidFacts[9] = "If you are having a lot of trouble breathing or are " +
                        "experiencing chest pain, you should seek medical care.";
    }

    #region setters and getters

    public void setRoomManager(RoomManager roomManager)
    {
        myRoomManager = roomManager;
    }

    public float getGoombaSpeed()
    {
        return speed;
    }

    public void setGoomaSpeed(float newSpeed)
    {
        speed = newSpeed;
    }

    public float getGoombaAccel()
    {
        return acceleration;
    }

    public void setGoombaAccel(float newAccel)
    {
        acceleration = newAccel;
    }

    public int getGoombaMaxHealth()
    {
        return maxHealth;
    }

    public void setGoombaMaxHealth(int newMax)
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

    public int getGoombaDamage()
    {
        return goombaDamage;
    }

    public void setGoombaDamage(int newDamage)
    {
        goombaDamage = newDamage;
    }

    #endregion

}
