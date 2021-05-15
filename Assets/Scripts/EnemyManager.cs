using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{

    [SerializeField] private GameObject goomba;
    [SerializeField] private GameObject guard;
    [SerializeField] private GameObject patrol;

    [SerializeField] private List<GameObject> enemies;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {

    }

    public List<GameObject> generateEnemies(GameObject[] spawnPositions, RoomManager roomManager)
    {

        int random;
        int index = 0;

        foreach (GameObject spawn in spawnPositions) 
        {
            random = Random.Range(1, 3);

            if (random == 1)
            {
                //creates a goomba and passes it the room its in
                enemies.Add(Instantiate(goomba, spawn.transform));
                enemies[index].GetComponent<GoombaAI>().setRoomManager(roomManager);
            } else if (random == 2) {
                //creates a guard and passes it the room its in
                enemies.Add(Instantiate(guard, spawn.transform));
                enemies[index].GetComponent<GuardAI>().setRoomManager(roomManager);
            } else if (random == 3)
            {
                //creates a patrol and passes it the room its in
                enemies.Add(Instantiate(patrol, spawn.transform));
                enemies[index].GetComponent<PatrolAI>().setRoomManager(roomManager);

                //sets the patrol point of the patrol to be bewteen two spawn points
                if (index == 0)
                {
                    enemies[index].GetComponent<PatrolAI>().setPoints(enemies[index].transform, enemies[index + 1].transform);
                } else
                {
                    enemies[index].GetComponent<PatrolAI>().setPoints(enemies[index].transform, enemies[index - 1].transform);
                }
            }

            index++;
        }

        return enemies;
    }

}
