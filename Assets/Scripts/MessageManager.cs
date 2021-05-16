using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageManager : MonoBehaviour
{

    [SerializeField] private float lifeStart;
    [SerializeField] private float lifeEnd;
    [SerializeField] private float lifeSpan;

    [SerializeField] private int enemyType;

    // Start is called before the first frame update
    void Start()
    {
        lifeStart = Time.time;
        lifeEnd = lifeStart + lifeSpan;

        foreach (Image BG in this.GetComponentsInChildren<Image>())
        {
            BG.color = new Color(BG.color.r, BG.color.g, BG.color.b, 0);
        }

        this.GetComponentInChildren<Text>().color = new Color(this.GetComponentInChildren<Text>().color.r,
            this.GetComponentInChildren<Text>().color.g, this.GetComponentInChildren<Text>().color.b, 0);
    }

    // Update is called once per frame
    void Update()
    {

        if (lifeStart - Time.time > -2)
        {
            foreach (Image BG in this.GetComponentsInChildren<Image>())
            {
                BG.color = new Color(BG.color.r, BG.color.g, BG.color.b, BG.color.a + .0015f);
            }

            this.GetComponentInChildren<Text>().color = new Color(this.GetComponentInChildren<Text>().color.r,
                this.GetComponentInChildren<Text>().color.g, this.GetComponentInChildren<Text>().color.b,
                this.GetComponentInChildren<Text>().color.a + .005f);
        }

        
        if (Time.time > lifeEnd)
        {
            foreach (Image BG in this.GetComponentsInChildren<Image>())
            {
                BG.color = new Color(BG.color.r, BG.color.g, BG.color.b, BG.color.a - .008f);
            }

            this.GetComponentInChildren<Text>().color = new Color(this.GetComponentInChildren<Text>().color.r,
                this.GetComponentInChildren<Text>().color.g, this.GetComponentInChildren<Text>().color.b,
                this.GetComponentInChildren<Text>().color.a - .025f);

            if (this.GetComponentInChildren<Text>().color.a <= .1f)
            {
                Destroy(gameObject);
            }

        }

    }


    public void SetEnemyType(int type)
    {

        //0 = goomba
        //1 = guard
        //2 = patrol
        enemyType = type;

    }

}
