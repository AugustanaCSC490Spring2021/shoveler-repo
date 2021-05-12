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


}
