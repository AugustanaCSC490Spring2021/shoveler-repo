﻿using System.Collections;
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
    private List<GameObject> rooms;


    [SerializeField]
    private int seed;


    // Start is called before the first frame update
    void Awake()
    {
        Random.InitState(seed);
        GameObject temp = Instantiate(roomPrefab, new Vector3(0, 0, 0), roomPrefab.transform.rotation);
        temp.name = "EntryRoom";
        temp.GetComponent<RoomManager>().enableEntryRoomIndicator();
        rooms.Add(temp);

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        creatingDungeon();
    }

    /// <summary>
    /// creates rooms using the array of spawnpoints that is added to whenever a room is spawned, chooses a random room and calls to create
    /// a new room at that position using createRoom()
    /// </summary>
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
            rooms[rooms.Count - 1].GetComponent<RoomManager>().enableExitRoomIndicator();
        }

    }

    /// <summary>
    /// creates room at the transform.position of the spawnpoint that was given, along with the direction of the door that should be enabled.
    /// </summary>
    /// <param name="doorDir"></param> - direction door should be enabled in room
    /// <param name="spawnpoint"></param> - position where the new room should be made
    void spawnRoom(int doorDir, GameObject spawnpoint)
    {

        int[] doorOpposites = { 1, 0, 3, 2 };
        spawnpoint.GetComponentInParent<RoomManager>().enableDoor(doorOpposites[doorDir]);
        GameObject temp = Instantiate(roomPrefab, spawnpoint.transform.position, roomPrefab.transform.rotation);
        temp.GetComponent<RoomManager>().enableDoor(doorDir);
        temp.GetComponentInParent<RoomManager>().chooseWallPreset();
        numRooms++;
        rooms.Add(temp);


    }



}
