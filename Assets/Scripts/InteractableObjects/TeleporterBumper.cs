using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleporterBumper : InteractableObject
{
    private TeleporterManager manager;

    public float bumperForce;

    public override void Awake()
    {
        base.Awake();
        manager = GetComponentInParent<TeleporterManager>();
    }

    public override void BallHit(Ball ball, Collision2D collision)
    {
        base.BallHit(ball, collision);
        ball.theRB.AddForce(collision.GetContact(0).normal * bumperForce, ForceMode2D.Impulse);
        manager.Payout(ball, 1);
    }
}
