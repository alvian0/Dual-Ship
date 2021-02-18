using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FusionForm : MonoBehaviour
{
    public float Speed = 10;
    public GameObject p1, p2;
    public float BulletSpeed;
    public float FireRateSpeed = .3f;
    public GameObject bullets;
    public Transform muzzle1, muzzle2;
    public GameObject DefusionParticle;
    public float HP = 5;

    float FireRate;
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        FireRate = FireRateSpeed;
    }

    void Update()
    {
        if (FireRate <= 0)
        {
            GameObject bullet1 = Instantiate(bullets, muzzle1.position, Quaternion.identity);
            GameObject bullet2 = Instantiate(bullet1, muzzle2.position, Quaternion.identity);

            bullet1.GetComponent<Rigidbody2D>().AddForce(transform.up * BulletSpeed, ForceMode2D.Impulse);
            bullet2.GetComponent<Rigidbody2D>().AddForce(transform.up * BulletSpeed, ForceMode2D.Impulse);
            FireRate = FireRateSpeed;
        }

        else
        {
            FireRate -= Time.deltaTime;
        }

        moveConditions();
        DestroyCondition();
    }

    void moveConditions()
    {
        if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.LeftArrow))
        {
            rb.AddForce(-transform.right * Speed * 100 * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.RightArrow))
        {
            rb.AddForce(transform.right * Speed * 100 * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.UpArrow))
        {
            rb.AddForce(transform.up * Speed * 100 * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.DownArrow))
        {
            rb.AddForce(-transform.up * Speed * 100 * Time.deltaTime);
        }
    }

    void DestroyCondition()
    {
        if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.RightArrow))
        {
            SplitUps();
        }
        
        if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.LeftArrow))
        {
            SplitUps();
        }

        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.DownArrow))
        {
            SplitUps();
        }

        if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.UpArrow))
        {
            SplitUps();
        }
    }

    void SplitUps()
    {
        GameObject Player1 = Instantiate(p1, new Vector2(transform.position.x + -1.5f, transform.position.y), Quaternion.identity);
        GameObject Player2 = Instantiate(p2, new Vector2(transform.position.x + 1.5f, transform.position.y), Quaternion.identity);

        Player1.GetComponent<Rigidbody2D>().AddForce(transform.right * -10, ForceMode2D.Impulse);
        Player2.GetComponent<Rigidbody2D>().AddForce(transform.right * 10, ForceMode2D.Impulse);

        Camera.main.GetComponent<RippleCall>().ripple();
        Instantiate(DefusionParticle, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Asteroid"))
        {
            collision.gameObject.GetComponent<AsteroidFall>().StatterEffect();
        }

        if (collision.gameObject.CompareTag("Enemy2") || collision.gameObject.CompareTag("Enemy3"))
        {
            HP--;

            if (HP <= 0)
            {
                SplitUps();
            }
        }
    }
}