using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{

    [SerializeField]
    private GameObject[] doors;
    [SerializeField]
    private GameObject[] walls;
    [SerializeField]
    private GameObject entryRoomIndicator;
    [SerializeField]
    private GameObject exitRoomIndicator;
    [SerializeField]
    private GameObject[] insideWallPresets;
    [SerializeField]
    private List<GameObject> spawnpoints;
    [SerializeField]
    private GameObject[] enemySpawnPoints;
    [SerializeField]
    private List<GameObject> enemies;

    //
    private bool playerInRoom; 

    private void Awake()
    {
        for (int i = 0; i < spawnpoints.Count; i++)
        {
            GameObject.Find("DungeonManager").GetComponent<DungeonManager>().spawnpointsList.Add(spawnpoints[i]);
        }
        playerInRoom = false;
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (enemiesAllDead())
        {
            //to-do: open doors here

        }
    }

    //enables door facing in int direction, 0 for up, 1 for down, 2 for left, 3 for right
    public void enableDoor(int direction)
    {
        doors[direction].SetActive(true);
        walls[direction].SetActive(false);

    }

    public void enableEntryRoomIndicator()
    {
        entryRoomIndicator.SetActive(true);
    }

    public void enableExitRoomIndicator()
    {
        exitRoomIndicator.SetActive(true);
    }


    public void removeSpawnpoint(GameObject spawnpoint)
    {
        spawnpoints.Remove(spawnpoint);
    }

    public void chooseWallPreset()
    {
        int whichPreset = Random.Range(-5, insideWallPresets.Length);
        if (whichPreset >= 0)
             insideWallPresets[whichPreset].SetActive(true);
    }

    public void spawnEnemies()
    {
        //to-do make an array based on the difficulty settings so that there are more/less spawn points each time


        //takes in an array of spawn points and the RoomManager, and returns a list of enemy GameObjects
        enemies = this.GetComponent<EnemyManager>().generateEnemies(enemySpawnPoints,this.GetComponent<RoomManager>());
    }

    //checks if all enemies have removed themselves from the list
    public bool enemiesAllDead ()
    {
        return enemies.Count == 0;
    }

    //removes dead enemies from the list
    public void removeDeadEnemy(GameObject enemy)
    {
        enemies.Remove(enemy);
    }

}
