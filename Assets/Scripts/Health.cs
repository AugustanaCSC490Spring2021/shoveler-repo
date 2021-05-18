using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    private int health = 100;
public int maxShield = 150;
public int shield = 0;
    private bool turn = false;


    void Start()
    {
    //    if (this.gameObject.tag == "player")
    //    {
    //        turn = true;
    //    }
    }

    public void Damage(int damageAmount)
    {
        if (this.gameObject.tag == "Player" && shield > 0)
        {

            decreaseShield(damageAmount);
            Debug.Log("are you getting this" + shield);

        }
        else
        {
            health -= damageAmount;
        }
        //health -= damageAmount;
    }

    public int GetHealth()
    {
        return health;
    }

    public void AddHealth(int healAmount)
    {
        health += healAmount;
    }

    public void SetHealth(int health)
    {
        this.health = health;
    }
    public void setShield(int shieldValue)
    {

                   shield = shieldValue;

    }
    public int GetShield()
    {
        return shield;

    }
    public int GetMaxShield()
    {
        return maxShield;

    }

    public void increaseShild(int shieldGain)
    {
        if (turn)
        {
            shield += shieldGain;
        }
        else
        {
            return;
        }
    }

    public void decreaseShield(int damage)
    {
        shield -= damage;
        Debug.Log("decreaseShield" + shield);
        // if (turn)
        //{
        //  shield -= damage;
        //}
        //else
        // {
        //    return;
        //}

    }

}
