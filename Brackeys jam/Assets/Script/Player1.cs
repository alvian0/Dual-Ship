using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1 : MonoBehaviour
{
    public int PlayerIndex = 1;
    public float Speed = 5;
    public string HorizontalAxis, VerticalAxis;
    public float FireRateSpeed = .3f;
    public float BulletImpact;
    public GameObject Bullets;
    public Transform Muzzle;

    GameManager manager;
    Rigidbody2D rb;
    Vector2 move;
    float FireRate;

    void Start()
    {
        manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>();
        FireRate = FireRateSpeed;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
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

        move = new Vector2(Input.GetAxisRaw(HorizontalAxis),Input.GetAxisRaw(VerticalAxis));
        rb.AddForce(move.normalized * Speed * 100 * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player1")
        {
            manager.Summon();
            Destroy(collision.gameObject);
            Camera.main.GetComponent<RippleCall>().ripple();
            Destroy(gameObject);
        }

        if (collision.gameObject.tag == "Player2")
        {
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Asteroid"))
        {
            collision.gameObject.GetComponent<AsteroidFall>().StatterEffect();

            if (PlayerIndex == 1)
            {
                manager.Hp1--;
            }

            else if (PlayerIndex == 2)
            {
                manager.Hp2--;
            }
        }
    }
}
