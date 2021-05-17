using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DungeonManager : MonoBehaviour
{


    private bool startDunGen;

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
        startDunGen = false;
        beginDungeonGeneration(1004, 0);
    }

    private void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        if (startDunGen)
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
            rooms[rooms.Count - 1].GetComponent<RoomManager>().enableExitRoom();
            startDunGen = false;
            rooms[0].GetComponent<NavMeshSurface>().BuildNavMesh();
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


    /// <summary>
    /// Allows the dungeon generator to beign generating the dungeon, setting the max rooms based on what level the player is on,
    /// and setting the seed to a given seed
    /// </summary>
    /// <param name="seed"></param>
    /// <param name="level"></param>
    public void beginDungeonGeneration(int seed, int level)
    {
        startDunGen = true;
        this.seed = seed;
        Random.InitState(seed);
        maxRooms = level * 3 + 5;
        if (maxRooms > 20)
            maxRooms = 20;

        GameObject temp = Instantiate(roomPrefab, new Vector3(0, 0, 0), roomPrefab.transform.rotation);
        temp.name = "EntryRoom";
        temp.GetComponent<RoomManager>().enableEntryRoomIndicator();
        temp.GetComponent<RoomManager>().setRoomCleared(true);
        rooms.Add(temp);

    }

    public bool checkIfLevelCleared()
    {
        for (int i = 0; i < rooms.Count; i++)
        {
            if (!rooms[i].GetComponent<RoomManager>().getRoomCleared())
                return false;
        }
        return true;
    }

}
