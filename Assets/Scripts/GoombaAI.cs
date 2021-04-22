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
    private float waitTime = 3;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        if (playerObj == null)
        {
            //we can substitute "Player" for whatever we name our player character
            playerObj = GameObject.FindGameObjectWithTag("Player");
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (waitTime < Time.time)
        {
            //moves this object towards the players position
            agent.SetDestination(playerObj.transform.position);
        }
    }
}
