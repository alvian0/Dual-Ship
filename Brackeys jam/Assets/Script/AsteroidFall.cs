using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidFall : MonoBehaviour
{
    public int HP = 3;
    public GameObject StatterVersion;

    void Start()
    {

    }

    void Update()
    {
        if (transform.position.y <= -9)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            HP--;

            if (HP <= 0)
            {
                StatterEffect();
            }
        }
    }

    public void StatterEffect()
    {
        Instantiate(StatterVersion, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
