using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GoombaAI : MonoBehaviour
{

    #region variables

    [SerializeField] private GameObject playerObj;
    [SerializeField] private Vector3 playerPos;
    [SerializeField] private float speed;
    [SerializeField] private Health health;
    [SerializeField] private int playerDamage;

    #endregion


    // Start is called before the first frame update
    void Start()
    {
        //we can substitute "Player" for whatever we name our player character
        playerObj = GameObject.FindGameObjectWithTag("Player");
        playerPos = playerObj.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (health.GetHealth() <= 0)
        {
            //temporary line to simply delete the enemy when it is killed.
            Destroy(gameObject);

            //to-do add some way to broadcast the death of this enemy so that
            //we might open the doors upon there being no enemies left

        }

        playerPos = playerObj.transform.position;
        Debug.Log("Uodate is running");

        //moves this object towards the players position
        transform.position = Vector3.MoveTowards(transform.position, playerPos, Time.deltaTime * speed);

    }

    /*
    private void OnCollisionEnter(Collision collision)
    {
        health.Damage(playerDamage);
    }
    */
}
