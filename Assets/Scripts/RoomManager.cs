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
    private GameObject[] insideWallPresets;
    [SerializeField]
    private List<GameObject> spawnpoints;

    private void Awake()
    {
        for (int i = 0; i < spawnpoints.Count; i++)
        {
            GameObject.Find("DungeonManager").GetComponent<DungeonManager>().spawnpointsList.Add(spawnpoints[i]);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
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

    public void removeSpawnpoint(GameObject spawnpoint)
    {
        spawnpoints.Remove(spawnpoint);
    }

    public void chooseWallPreset()
    {
        int whichPreset = Random.Range(-1, insideWallPresets.Length);
        if (whichPreset >= 0)
             insideWallPresets[whichPreset].SetActive(true);
    }
}
