using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointTracker : MonoBehaviour
{
    //int with what direction a door is needed, 0 for up, 1 for down, 2 for left, 3 for right
    public int doorNeeded;

    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("DungeonManager").GetComponent<DungeonManager>().spawnpointsList.Add(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Deleted " + gameObject);
        if (other.CompareTag("RoomCollider")) {
            GameObject.Find("DungeonManager").GetComponent<DungeonManager>().spawnpointsList.Remove(gameObject);
            Destroy(gameObject);
        }
    }


}
