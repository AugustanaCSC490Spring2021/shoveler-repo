using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    private int health = 100;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Damage(int damageAmount)
    {
        health -= damageAmount;
    }

    public int GetHealth()
    {
        return health;
    }

    public void AddHealth(int healAmount)
    {
        health += healAmount;
    }
}
