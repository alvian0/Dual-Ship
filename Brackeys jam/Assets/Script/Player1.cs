using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Player1 : MonoBehaviour
{
    public int PlayerIndex = 1;
    public float Speed = 5;
    public string HorizontalAxis, VerticalAxis;
    public float FireRateSpeed = .3f;
    public float BulletImpact;
    public GameObject Bullets;
    public Transform Muzzle;
    public float ImuneTime = 1f;
    public LayerMask WhatIsEnemy;
    public float EnemyForce;

    [Range(0, 100), SerializeField]
    float CurrentHp;
    [SerializeField]
    float ImuneDuration;
    [SerializeField]
    bool Imune = false;

    GameManager manager;
    Rigidbody2D rb;
    Vector2 move;
    float FireRate;
    Animator anim;

    void Start()
    {
        manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        FireRate = FireRateSpeed;
        ImuneDuration = ImuneTime;
    }

    void Update()
    {
        if (PlayerIndex == 1) CurrentHp = manager.Hp1;
        else if (PlayerIndex == 2) CurrentHp = manager.Hp2;

        if (FireRate <= 0)
        {
            GameObject bull = Instantiate(Bullets, Muzzle.transform.position, Quaternion.identity);
            bull.GetComponent<Rigidbody2D>().AddForce(transform.up * BulletImpact, ForceMode2D.Impulse);
            FireRate = FireRateSpeed;
        }

        else
        {
            FireRate -= Time.deltaTime;
        }

        if (Imune)
        {
            ImuneDuration -= Time.deltaTime;

            if (ImuneDuration <= 0)
            {
                ImuneDuration = ImuneTime;
                anim.SetBool("Hit", false);
                Imune = false;
            }
        }

        move = new Vector2(Input.GetAxisRaw(HorizontalAxis),Input.GetAxisRaw(VerticalAxis));
        rb.AddForce(move.normalized * Speed * 100 * Time.deltaTime);
    }

    public void Hurt()
    {
        Imune = true;
        anim.SetBool("Hit", true);

        if (PlayerIndex == 1)
        {
            manager.Hp1--;
        }

        else if (PlayerIndex == 2)
        {
            manager.Hp2--;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player1")
        {
            if (Imune || collision.gameObject.GetComponent<Player1>().ImuneChek())
            {
                Debug.Log("Imune");
                return;
            }

            manager.Summon();
            Destroy(collision.gameObject);
            Camera.main.GetComponent<RippleCall>().ripple();
            Destroy(gameObject);
        }

        if (collision.gameObject.tag == "Player2")
        {
            if (Imune || collision.gameObject.GetComponent<Player1>().ImuneChek())
            {
                Debug.Log("Imune");
                return;
            }

            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Asteroid"))
        {
            if (Imune)
            {
                Debug.Log("Imune");
                return;
            }

            collision.gameObject.GetComponent<AsteroidFall>().StatterEffect();

            Hurt();
        }

        if (collision.gameObject.CompareTag("Enemy2"))
        {
            if (Imune)
            {
                Debug.Log("Imune");
                return;
            }

            Vector2 ForceDirection = collision.gameObject.transform.position - transform.position;
            rb.AddForce(ForceDirection.normalized * -EnemyForce, ForceMode2D.Impulse);
            Hurt();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy1"))
        {
            if (Imune)
            {
                Debug.Log("Imune");
                return;
            }

            Hurt();
        }

        if (collision.CompareTag("Dynamite"))
        {
            if (Imune)
            {
                Debug.Log("Imune");
                return;
            }

            collision.GetComponent<Dynamite>().Explode();
        }

        if (collision.CompareTag("EnemyProjectile"))
        {
            if (Imune)
            {
                Debug.Log("Imune");
                return;
            }

            Hurt();
        }

        if (collision.CompareTag("Laser"))
        {
            if (Imune)
            {
                Debug.Log("Imune");
                return;
            }

            Hurt();
        }
    }

    public bool ImuneChek()
    {
        return Imune;
    }
}