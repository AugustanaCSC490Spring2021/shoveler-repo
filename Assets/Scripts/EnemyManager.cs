using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{

    [SerializeField] private GameObject goomba;
    [SerializeField] private GameObject guard;
    [SerializeField] private GameObject patrol;

    [SerializeField] private List<GameObject> enemies;

    //maybe we can use this to open the doors?
    [SerializeField] private GameObject doors;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {

    }

    public List<GameObject> generateEnemies(GameObject[] spawnPositions)
    {

        int random;
        int index = 0;

        foreach (GameObject spawn in spawnPositions) 
        {
            random = Random.Range(1, 3);

            if (random == 1)
            {
                //enemies[index] = Instantiate(goomba, spawn.transform);
                enemies.Add(Instantiate(goomba, spawn.transform));
            } else if (random == 2) {
                //enemies[index] = Instantiate(guard, spawn.transform);
                enemies.Add(Instantiate(guard, spawn.transform));
            } else if (random == 3)
            {
                //enemies[index] = Instantiate(patrol, spawn.transform);
                enemies.Add(Instantiate(patrol, spawn.transform));
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
