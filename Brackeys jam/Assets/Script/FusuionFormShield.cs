using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FusuionFormShield : MonoBehaviour
{
    public float HP = 5;
    public GameObject ShieldDestroyPartilce;

    void Start()
    {
        
    }

    void Update()
    {
        transform.position = transform.parent.transform.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy1") || collision.gameObject.CompareTag("Enemy2") || collision.gameObject.CompareTag("Enemy3"))
        {
            HurtAndDestroy();
        }

        if (collision.gameObject.CompareTag("Asteroid"))
        {
            HurtAndDestroy();
        }

        if (collision.gameObject.CompareTag("EnemyProjectile"))
        {
            HurtAndDestroy();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemyProjectile"))
        {
            HurtAndDestroy();
            Destroy(collision.gameObject);
        }
    }

    void HurtAndDestroy()
    {
        HP--;

        if (HP <= 0)
        {
            Instantiate(ShieldDestroyPartilce, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
