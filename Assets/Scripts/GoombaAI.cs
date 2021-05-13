﻿using System.Collections;
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
            gameObject.SetActive(false);

            //to do: display covid factoid for period of time and then delete the enemy

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

    void covidFactGenerator()
    {

        /*
         * Sources:
         * https://www.worldometers.info/coronavirus/?utm_campaign=homeAdUOA?Si
         * https://www.goodrx.com/blog/flu-vs-coronavirus-mortality-and-death-rates-by-year/
         * https://www.cdc.gov/coronavirus/2019-ncov/symptoms-testing/symptoms.html
         */

        covidFacts[0] = "COVID-19 spreads mainly through respiratory\n" +
                        "droplets produced from sneezes and coughs.";
        covidFacts[1] = "COVID-19 is much more likely to spread when\n" +
                        "people are within 6ft of one another.";
        covidFacts[2] = "COVID-19 spreads easily from person to person.\n" +
                        "More so than Influenza, but less so than measles.";
        covidFacts[3] = "Community spread can occur when several people\n" +
                        "in an area are infected, and some are not sure how!";
        covidFacts[4] = "COVID-19 spreads mainly from person to person.\n" +
                        "It does not travel through mosquitos and ticks.";
        covidFacts[5] = "There have been 160 million cases of COIVD-19\n" +
                        "reported worldwide as of May, 2021.";
        covidFacts[6] = "COVID-19 has a mortality rate of roughly 3%,\n" +
                        "while Influenza(The Flu) sits at around 0.1%!";
        covidFacts[7] = "COVID-19 can cause you to temporarily lose\n" +
                        "your sense of smell!";
        covidFacts[8] = "Some common symptoms of COVID-19 are:\n" +
                        "fever, cough, shortness of breath, and headaches.";
        covidFacts[9] = "If you are having a lot of trouble breathing or are\n" +
                        "experiencing chest pain, you should seek medical care.";
    }

}
