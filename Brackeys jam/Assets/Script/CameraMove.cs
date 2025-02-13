﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public float Zoffset;
    public float Speed;
    public float MaxOrthograpicSize = 10f;

    GameObject p1, p2, FusionMode;
    Vector3 midPoint;
    float minOrthograpicSize;

    void Start()
    {
        p1 = GameObject.FindGameObjectWithTag("Player1");
        p2 = GameObject.FindGameObjectWithTag("Player2");
        minOrthograpicSize = Camera.main.orthographicSize;
    }

    void Update()
    {
        p1 = GameObject.FindGameObjectWithTag("Player1");
        p2 = GameObject.FindGameObjectWithTag("Player2");
        FusionMode = GameObject.FindGameObjectWithTag("FusionPlayer");

        if (p1 != null && p2 != null)
        {
            float Xpos = (p1.transform.position.x + p2.transform.position.x) / 2;
            float Ypos = (p1.transform.position.y + p2.transform.position.y) / 2;
            midPoint = new Vector3(Xpos, Ypos, Zoffset);

            float distance = Vector2.Distance(p1.transform.position, p2.transform.position) / 5;
            Camera.main.orthographicSize = Mathf.Clamp(minOrthograpicSize + distance, 5, MaxOrthograpicSize);
        }

        else if (FusionMode != null)
        {
            Camera.main.orthographicSize = MaxOrthograpicSize;
        }
    }

    private void FixedUpdate()
    {
        if (p1 != null && p2 != null)
        {
            Vector3 Smoothed = Vector3.Lerp(transform.position, midPoint, Speed);

            //transform.position = Smoothed;
        }
    }
}
