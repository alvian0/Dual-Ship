using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : MonoBehaviour
{
    public float FireRateSpeed = 1;
    public GameObject Bullets;
    public Transform muzzle;
    public float BulletImpact = 10;
    public float Hp = 15;
    public GameObject DeadParticle;

    float FireRate;
    EnemyCounter count;
    EnemySpawner Spawner;

    void Start()
    {
        Spawner = GameObject.FindGameObjectWithTag("ESpawn").GetComponent<EnemySpawner>();
        count = GameObject.FindGameObjectWithTag("Count").GetComponent<EnemyCounter>();
        FireRate = FireRateSpeed;
    }

    void Update()
    {
        if (Hp <= 0)
        {
            Dead();
        }

        if (FireRate <= 0)
        {
            Shoot();
            FireRate = FireRateSpeed;
        }

        else
        {
            FireRate -= Time.deltaTime;
        }
    }

    public void Shoot()
    {
        GameObject bull = Instantiate(Bullets, muzzle.position, Quaternion.identity);

        bull.GetComponent<Rigidbody2D>().AddForce(-transform.up * BulletImpact, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            Hp--;

            if (Hp <= 0)
            {
                Dead();
            }
        }
    }

    void Dead()
    {
        count.EnemyInGame.Remove(gameObject);
        Spawner.EmptySpawnPoint.Add(transform.position);
        Instantiate(DeadParticle, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}