using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutHole : InteractableObject
{

    public override void BallHit(Ball ball, Collider2D collision)
    {
        ball.gameObject.layer = 3;
        GetComponentInParent<MachineManager>().shooter.ShootBall(ball);
    }
}
