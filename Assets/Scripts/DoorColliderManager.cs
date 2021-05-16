using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorColliderManager : MonoBehaviour
{

    [SerializeField]
    private int doorDirection;
    private int moveAmount = 5;
    private Vector3[] directionArray;

    // Start is called before the first frame update
    void Start()
    {
        directionArray = new Vector3[] { Vector3.back, Vector3.forward, Vector3.right, Vector3.left};
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void movePlayer(GameObject player)
    {
        if (doorDirection == 0)
        {
            //down
            player.transform.position = new Vector3(gameObject.transform.position.x, player.transform.position.y, gameObject.transform.position.z-6.5f);
        }
        else if (doorDirection == 1)
        {
            //up
            player.transform.position = new Vector3(gameObject.transform.position.x, player.transform.position.y, gameObject.transform.position.z + 6.5f);
        } else if (doorDirection == 2)
        {
            player.transform.position = new Vector3(gameObject.transform.position.x + 6.5f, player.transform.position.y, gameObject.transform.position.z);
        } else if (doorDirection == 3)
        {
            player.transform.position = new Vector3(gameObject.transform.position.x - 6.5f, player.transform.position.y, gameObject.transform.position.z);
        }
    }


}
