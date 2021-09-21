using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiTargetBumper : MultiTarget
{
    public override void BallHit(Ball ball, Collision2D collision)
    {
        base.BallHit(ball, collision);
        ball.theRB.AddForce(collision.GetContact(0).normal * 10, ForceMode2D.Impulse);
        manager.Payout(ball, 1);

        Debug.Log("targetbumper");
    }
}
