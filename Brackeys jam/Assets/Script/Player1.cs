using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1 : MonoBehaviour
{
    public float Speed = 5;
    public string HorizontalAxis, VerticalAxis;
    public float FireRateSpeed = .3f;
    public float BulletImpact;
    public GameObject Bullets;
    public Transform Muzzle;

    Rigidbody2D rb;
    Vector2 move;
    float FireRate;

    void Start()
    {
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

        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(transform.up * 10, ForceMode2D.Impulse);
        }

        move = new Vector2(Input.GetAxisRaw(HorizontalAxis),Input.GetAxisRaw(VerticalAxis));
        rb.AddForce(move.normalized * Speed);
    }

    private void FixedUpdate()
    {
    }
}
