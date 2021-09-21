using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoPaddle : MonoBehaviour
{
    private Paddle paddle;


    private void Awake()
    {
        paddle = GetComponentInParent<Paddle>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.layer == 3 && paddle.isAuto)
        {
            StartCoroutine(paddle.AutoPaddle());
        }
    }
}
