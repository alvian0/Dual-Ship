using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3 : MonoBehaviour
{
    public float Hp;
    public float FireRateSpeed = 2;
    public GameObject Laser;
    public GameObject LaserPrep;
    public float BeamLenght = 10;
    public GameObject DeadParticle;
    public Transform muzzle;
    public GameObject StatterEffect;

    GameManager manager;
    AudioSource sfx;
    bool IsShootingLaser;
    float FireRate;
    EnemyCounter count;
    EnemySpawner Spawner;

    void Start()
    {
        manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>();
        sfx = GameObject.Find("EnemySFX").GetComponent<AudioSource>();
        Spawner = GameObject.FindGameObjectWithTag("ESpawn").GetComponent<EnemySpawner>();
        count = GameObject.FindGameObjectWithTag("Count").GetComponent<EnemyCounter>();
        FireRate = FireRateSpeed;
    }

    void Update()
    {
        if (FireRate <= 0)
        {
            if (!IsShootingLaser)
            {
                StartCoroutine(LaserBeam());
                return;
            }
        }

        else
        {
            FireRate -= Time.deltaTime;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            Hurt();
        }
    }

    void Hurt()
    {
        Hp--;

        if (Hp <= 0)
        {
            Instantiate(StatterEffect, transform.position, Quaternion.identity);
            Instantiate(DeadParticle, transform.position, Quaternion.identity);
            count.EnemyInGame.Remove(transform.parent.gameObject);
            sfx.Play();
            manager.GetScore();
            Spawner.EmptySpawnPoint.Add(transform.parent.transform.position);
            Destroy(transform.parent.gameObject);
        }
    }

    IEnumerator LaserBeam()
    {
        IsShootingLaser = true;
        GameObject LaserParent = Instantiate(LaserPrep, muzzle.position, Quaternion.identity, transform);
        GameObject lasers = Instantiate(Laser, muzzle.position, Quaternion.identity, LaserParent.transform);

        yield return new WaitForSeconds(1f);

        lasers.transform.localScale = new Vector3(lasers.transform.localScale.x, BeamLenght);

        yield return new WaitForSeconds(1f);

        FireRate = FireRateSpeed;
        IsShootingLaser = false;
        Destroy(LaserParent.gameObject);
    }
}
