using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public EnemyCounter count;
    public GameObject[] EnemyToSpawn;
    public Transform[] SpawnPos;
    public float SpawnRateSpeed = 5;
    public List<Vector3> EmptySpawnPoint;

    float SpawnRate;

    void Start()
    {
        SpawnRate = SpawnRateSpeed;
    }

    void Update()
    {
        if (SpawnRate <= 0)
        {
            if (count.EnemyInGame.Count == count.IngameEnemyLimit)
            {
                SpawnRate = SpawnRateSpeed;
                return;
            }

            int index = count.EnemyInGame.Count;

            if (EmptySpawnPoint.Count != 0)
            {
                foreach (GameObject enemiess in count.EnemyInGame)
                {
                    if (enemiess.transform.position == EmptySpawnPoint[0])
                    {
                        EmptySpawnPoint.RemoveAt(0);
                    }
                }
                
                Spawn(EmptySpawnPoint[0]);
                EmptySpawnPoint.RemoveAt(0);
            }

            else
            {
                Spawn(SpawnPos[index].transform.position);
            }
        }

        else
        {
            SpawnRate -= Time.deltaTime;
        }
    }

    void Spawn(Vector3 SpawnPoss)
    {
        GameObject enemy = Instantiate(EnemyToSpawn[Random.Range(0, EnemyToSpawn.Length)], SpawnPoss, Quaternion.identity);
        count.EnemyInGame.Add(enemy);
        SpawnRate = SpawnRateSpeed;
    }
}
