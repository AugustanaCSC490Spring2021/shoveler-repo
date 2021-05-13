using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonManager : MonoBehaviour
{

    [SerializeField]
    private int maxRooms;
    private int numRooms = 1;


    public List<GameObject> spawnpointsList;

    [SerializeField]
    private GameObject roomPrefab;


    [SerializeField]
    private int seed;


    // Start is called before the first frame update
    void Awake()
    {
        Random.InitState(seed);
        GameObject temp = Instantiate(roomPrefab, new Vector3(0, 0, 0), roomPrefab.transform.rotation);
        temp.name = "EntryRoom";
        temp.GetComponent<RoomManager>().enableEntryRoomIndicator();


    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        creatingDungeon();
    }

    void creatingDungeon()
    {
        if (spawnpointsList.Count != 0 && numRooms != maxRooms)
        {
            int randSpawnpoint = Random.Range(0, spawnpointsList.Count);
            GameObject temp = spawnpointsList[randSpawnpoint];
            int doorDir = temp.GetComponent<SpawnPointTracker>().doorNeeded;
            spawnRoom(doorDir, temp);

        }
        else if (spawnpointsList.Count > 0 && numRooms == maxRooms)
        {
            int tempLoopCount = spawnpointsList.Count;
            for (int i = 0; i < tempLoopCount; i++)
            {
                GameObject temp = spawnpointsList[0];
                spawnpointsList.RemoveAt(0);
                Destroy(temp);
            }
        }
    }

    void spawnRoom(int doorDir, GameObject spawnpoint)
    {

        int[] doorOpposites = { 1, 0, 3, 2 };
        spawnpoint.GetComponentInParent<RoomManager>().enableDoor(doorOpposites[doorDir]);
        GameObject temp = Instantiate(roomPrefab, spawnpoint.transform.position, roomPrefab.transform.rotation);
        temp.GetComponent<RoomManager>().enableDoor(doorDir);
        temp.GetComponentInParent<RoomManager>().chooseWallPreset();
        numRooms++;


    }



}
