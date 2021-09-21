using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RolloverTarget : InteractableObject
{
    private RolloverManager manager;

    public override void Awake()
    {
        base.Awake();
        manager = GetComponentInParent<RolloverManager>();
    }

    public override void BallHit(Ball ball, Collider2D collision)
    {
        manager.AddHit(ball);
        manager.Payout(ball, 1);
    }

}
