using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPack : MonoBehaviour
{
    private GameObject playerObject;
    private Health playerHealthScript;

    private void onCollisionEnter(Collider Other) {
        playerObject = GameObject.FindGameObjectWithTag("Player");
        playerHealthScript = playerObject.GetComponent<Health>();
        // make the health pack disappear.
    }
    

    private void Awake()
    {
        
    }
    }
