using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        Physics2D.IgnoreLayerCollision(10, 9);
    }
}
