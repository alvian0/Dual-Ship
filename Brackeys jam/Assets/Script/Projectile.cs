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
        Instantiate(DisapearParticle, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
