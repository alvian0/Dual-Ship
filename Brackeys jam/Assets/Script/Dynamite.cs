using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dynamite : MonoBehaviour
{
    public GameObject DynamiteParticle;
    public float Radius = 5;
    public float Impact;
    public float Hp = 3;

    AudioSource SFX;

    private void Start()
    {
        SFX = GameObject.Find("DynamiteSFX").GetComponent<AudioSource>();
        transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, Random.Range(0, 360));
    }

    public void Explode()
    {
        Collider2D[] coll = Physics2D.OverlapCircleAll(transform.position, Radius);

        foreach (Collider2D collisions in coll)
        {

            if (collisions.gameObject.GetComponent<Rigidbody2D>() != null)
            {
                if (!collisions.gameObject.CompareTag("EnemyProjectile") && !collisions.gameObject.CompareTag("Projectile"))
                {
                    Vector3 ImpactDirectios = transform.position - collisions.gameObject.transform.position;

                    collisions.gameObject.GetComponent<Rigidbody2D>().AddForce(ImpactDirectios.normalized * -Impact, ForceMode2D.Impulse);
                }
            }

            if (collisions.gameObject.CompareTag("Player1"))
            {
                GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>().Hp1--;
                collisions.gameObject.GetComponent<Player1>().Hurt();
            }

            else if (collisions.gameObject.CompareTag("Player2"))
            {
                GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>().Hp2--;
                collisions.gameObject.GetComponent<Player1>().Hurt();
            }

            if (collisions.gameObject.CompareTag("Enemy2"))
            {
                collisions.GetComponent<Enemy2>().Hp -= 2;
            }
        }

        SFX.Play();

        Instantiate(DynamiteParticle, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, Radius);
    }
}