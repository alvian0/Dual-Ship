using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject FusionForms;

    [Range(1, 100)]
    public float Hp1 = 100;
    [Range(1, 100)]
    public float Hp2 = 100;

    GameObject p1, p2;

    void Start()
    {
        p1 = GameObject.FindGameObjectWithTag("Player1");
        p2 = GameObject.FindGameObjectWithTag("Player2");
    }

    void Update()
    {
        Physics2D.IgnoreLayerCollision(10, 9);

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

        Instantiate(FusionForms, new Vector2(XPos, YPos), Quaternion.identity);
    }
}
