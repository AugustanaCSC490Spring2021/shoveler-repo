using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{

    [SerializeField] private GameObject goomba;
    [SerializeField] private GameObject guard;
    [SerializeField] private GameObject patrol;

    [SerializeField] private List<GameObject> enemies;
    [SerializeField] private float difficultyMultiplier;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {

    }

    public List<GameObject> generateEnemies(GameObject[] spawnPositions, RoomManager roomManager, int difficulty)
    {

        int random;
        int index = 0;

        difficultyMultiplier = (float)(1 + (0.25 * (difficulty - 1)));

        foreach (GameObject spawn in spawnPositions) 
        {
            random = Random.Range(1, 3);
            if (random == 1)
            {
                //creates a goomba and passes it the room its in
                enemies.Add(Instantiate(goomba, spawn.transform.position, spawn.transform.rotation));
                enemies[index].GetComponent<GoombaAI>().setRoomManager(roomManager);

                //adjust the speed based on the difficulty multiplier
                enemies[index].GetComponent<GoombaAI>().setGoomaSpeed(enemies[index].GetComponent<GoombaAI>().getGoombaSpeed() * difficultyMultiplier);

                //adjust the acceleration based on the difficulty multiplier
                enemies[index].GetComponent<GoombaAI>().setGoombaAccel(enemies[index].GetComponent<GoombaAI>().getGoombaAccel() * difficultyMultiplier);

                //adjust the attack speed based on the difficulty multiplier
                enemies[index].GetComponent<GoombaAI>().setAttackSpeedInSeconds(enemies[index].GetComponent<GoombaAI>().getAttackSpeedInSeconds() / difficultyMultiplier);

            } else if (random == 2) {
                //creates a guard and passes it the room its in
                enemies.Add(Instantiate(guard, spawn.transform.position, spawn.transform.rotation));
                enemies[index].GetComponent<GuardAI>().setRoomManager(roomManager);

                //adjust the speed based on the difficulty multiplier
                enemies[index].GetComponent<GuardAI>().setGuardSpeed(enemies[index].GetComponent<GuardAI>().getGuardSpeed() * difficultyMultiplier);

                //adjust the acceleration based on the difficulty multiplier
                enemies[index].GetComponent<GuardAI>().setGuardAccel(enemies[index].GetComponent<GuardAI>().getGuardAccel() * difficultyMultiplier);

                //adjust the attack speed based on the difficulty multiplier
                enemies[index].GetComponent<GuardAI>().setAttackSpeedInSeconds(enemies[index].GetComponent<GuardAI>().getAttackSpeedInSeconds() / difficultyMultiplier);

            }
            else if (random == 3)
            {
                //creates a patrol and passes it the room its in
                enemies.Add(Instantiate(patrol, spawn.transform.position,spawn.transform.rotation));
                enemies[index].GetComponent<PatrolAI>().setRoomManager(roomManager);

                //adjust the speed based on the difficulty multiplier
                enemies[index].GetComponent<PatrolAI>().setPatrolSpeed(enemies[index].GetComponent<PatrolAI>().getPatrolSpeed() * difficultyMultiplier);

                //adjust the acceleration based on the difficulty multiplier
                enemies[index].GetComponent<PatrolAI>().setPatrolAccel(enemies[index].GetComponent<PatrolAI>().getPatrolAccel() * difficultyMultiplier);

                //adjust the attack speed based on the difficulty multiplier
                enemies[index].GetComponent<PatrolAI>().setAttackSpeedInSeconds(enemies[index].GetComponent<PatrolAI>().getAttackSpeedInSeconds() / difficultyMultiplier);

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
