using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public float minShootForce;
    public float maxShootForce;

    public void ShootBall(Ball ball)
    {
        ball.transform.position = transform.position;
        ball.theRB.velocity = Vector2.zero;
        ball.theRB.AddForce(Vector2.up * Random.Range(minShootForce, maxShootForce), ForceMode2D.Impulse);

    }
}
