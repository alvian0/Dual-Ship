using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public float LifeTime = 3;
    void Start()
    {
        Destroy(gameObject, LifeTime);
    }

    void Update()
    {
        
    }
}
