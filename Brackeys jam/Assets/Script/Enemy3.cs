using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3 : MonoBehaviour
{
    public float Hp;
    public float FireRateSpeed = 2;
    public GameObject Laser;
    public float BeamLenght = 10;
    public GameObject DeadParticle;

    bool IsShootingLaser;
    float FireRate;

    void Start()
    {
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
            Instantiate(DeadParticle, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    IEnumerator LaserBeam()
    {
        IsShootingLaser = true;
        GameObject lasers = Instantiate(Laser, transform.position, Quaternion.identity, transform);

        yield return new WaitForSeconds(1f);

        lasers.transform.localScale = new Vector3(lasers.transform.localScale.x, BeamLenght);

        yield return new WaitForSeconds(1f);

        FireRate = FireRateSpeed;
        IsShootingLaser = false;
        Destroy(lasers.gameObject);
    }
}
