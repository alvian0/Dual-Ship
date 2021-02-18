using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatterObject : MonoBehaviour
{
    public float Impact = 3;

    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        float Xpos = 0;
        float Ypos = 0;

        for (int i = 0; i < transform.parent.childCount; i++)
        {
            Xpos += transform.parent.GetChild(i).gameObject.transform.position.x;
            Ypos += transform.parent.GetChild(i).gameObject.transform.position.y;
        }

        Vector3 Mid = new Vector2(Xpos / transform.parent.childCount, Ypos / transform.parent.childCount);

        Vector2 ImpactDirection = transform.position - Mid;

        rb.AddForce(ImpactDirection.normalized * Impact, ForceMode2D.Impulse);
    }
}
