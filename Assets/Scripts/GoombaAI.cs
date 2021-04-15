using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GoombaAI : MonoBehaviour
{

    #region variables

    [SerializeField] private GameObject playerObj = null;
    [SerializeField] private Vector3 playerPos;
    [SerializeField] private float speed;
    [SerializeField] private NavMeshAgent agent;

    #endregion


    // Start is called before the first frame update
    void Start()
    {


        //agent = GetComponent<NavMeshAgent>();

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
        //moves this object towards the players position
        transform.position = Vector3.MoveTowards(transform.position, playerPos, Time.deltaTime * speed);

    }
}
