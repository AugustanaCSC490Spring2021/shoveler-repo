using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    // Found on gamestart
    private GameObject playerObject; 
    private PlayerHealth playerHealthScript; 

    // Assigned in editor
    public Text healthText;


    // Start is called before the first frame update
    void Start()
    {
        playerObject = GameObject.FindGameObjectWithTag("Player");
        playerHealthScript = playerObject.GetComponent<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        healthText.text = "Health: " + playerHealthScript.GetHealth();
    }
}
