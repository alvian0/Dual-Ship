using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject FusionForms;
    public GameObject DefusionEffect;
    public Image Hpbar1, HpBar2;

    [Range(1, 100)]
    public float Hp1 = 100;
    [Range(1, 100)]
    public float Hp2 = 100;

    GameObject p1, p2;
    float HpLimit, HplImit2;

    void Start()
    {
        HpLimit = Hp1;
        HplImit2 = Hp2;
        p1 = GameObject.FindGameObjectWithTag("Player1");
        p2 = GameObject.FindGameObjectWithTag("Player2");
    }

    void Update()
    {
        Physics2D.IgnoreLayerCollision(10, 9);
        Physics2D.IgnoreLayerCollision(11, 10);

        Hpbar1.fillAmount = Hp1 / HpLimit;
        HpBar2.fillAmount = Hp2 / HplImit2;

        if (Hp1 <= 0 || Hp2 <= 0)
        {
            DestryPlayer();
        }
    }

    public void DestryPlayer()
    {
        p1 = GameObject.FindGameObjectWithTag("Player1");
        p2 = GameObject.FindGameObjectWithTag("Player2");

        Destroy(p1);
        Destroy(p2);
    }

    public void Summon()
    {
        p1 = GameObject.FindGameObjectWithTag("Player1");
        p2 = GameObject.FindGameObjectWithTag("Player2");

        float XPos = (p1.transform.position.x + p2.transform.position.x) / 2;
        float YPos = (p1.transform.position.y + p2.transform.position.y) / 2;

        Instantiate(DefusionEffect, new Vector2(XPos, YPos), Quaternion.identity);
        Instantiate(FusionForms, new Vector2(XPos, YPos), Quaternion.identity);
    }
}
