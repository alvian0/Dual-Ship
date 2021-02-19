using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCounter : MonoBehaviour
{
    public float IngameEnemyLimit;
    public List<GameObject> EnemyInGame;
    public float DespawnEnemy;
    public EnemySpawner Spawner;

    float DespawnTime;

    private void Start()
    {
        DespawnTime = DespawnEnemy;
    }

    private void Update()
    {
        if (EnemyInGame.Count == IngameEnemyLimit)
        {
            if (DespawnTime <= 0)
            {
                GameObject ToDestroy = EnemyInGame[0];

                if (ToDestroy.CompareTag("Enemy2") || ToDestroy.CompareTag("Enemy3"))
                {
                    StartCoroutine(DestroyToLongObject(ToDestroy));
                    DespawnTime = DespawnEnemy;
                }

                else
                {
                    Spawner.EmptySpawnPoint.Add(ToDestroy.transform.position);
                    EnemyInGame.Remove(ToDestroy);
                    Destroy(ToDestroy);
                    DespawnTime = DespawnEnemy;
                }
            }

            else
            {
                DespawnTime -= Time.deltaTime;
            }
        }
    }

    IEnumerator DestroyToLongObject(GameObject theObject)
    {
        theObject.GetComponent<Animator>().SetTrigger("Out");

        yield return new WaitForSeconds(2);

        Spawner.EmptySpawnPoint.Add(theObject.transform.position);
        EnemyInGame.Remove(theObject);
        Destroy(theObject);
    }
}
