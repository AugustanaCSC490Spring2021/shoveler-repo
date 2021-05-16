using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shield : MonoBehaviour
{
    public int shield=0;
    public int shieldPerTouch;
    public Slider slider;

    GameObject PlayerGameObject;
    Health PlayerHealth;
    // Start is called before the first frame update
    void SetShield() {
        slider.value = shield;
    }
    void IncreaseShield()
    {
        shield += shieldPerTouch;
    }

    // Update is called once per frame
    void DecreaseShield()
    {
        shield += shieldPerTouch;
    }
}
