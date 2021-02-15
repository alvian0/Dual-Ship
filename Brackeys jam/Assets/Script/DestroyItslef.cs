using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyItslef : MonoBehaviour
{
    public float DestroyTime = 2;
    void Start()
    {
        Destroy(gameObject, DestroyTime);
    }

}
