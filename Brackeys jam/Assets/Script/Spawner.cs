using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public float SpawnRateSpeed = 2;
    public float MaxX, MinX, MaxY, MinY;
    public GameObject[] objectlist;

    float SpawnRate;

    void Start()
    {
        SpawnRate = SpawnRateSpeed;
    }

    void Update()
    {
        if (SpawnRate <= 0)
        {
            Instantiate(
                objectlist[Random.Range(0, objectlist.Length)], 
                new Vector2(Random.Range(MinX, MaxX), Random.Range(MinY, MaxY)), 
                Quaternion.identity
                );
            SpawnRate = SpawnRateSpeed;
        }

        else
        {
            SpawnRate -= Time.deltaTime;
        }
    }
}
