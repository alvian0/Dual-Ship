using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float LifeTime;
    public GameObject DisapearParticle;

    void Start()
    {
        Destroy(gameObject, LifeTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        effectAndDestroy();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Dynamite"))
        {
            collision.GetComponent<Dynamite>().Hp--;

            if (collision.GetComponent<Dynamite>().Hp <= 0)
            {
                collision.GetComponent<Dynamite>().Explode();
            }
        }

        effectAndDestroy();
    }

    void effectAndDestroy()
    {
        Instantiate(DisapearParticle, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
