using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : InteractableObject
{
    private TargetManager manager;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        manager = GetComponentInParent<TargetManager>();
    }

    public override void BallHit(Ball ball, Collision2D collision)
    {
        base.BallHit(ball, collision);
        manager.Payout(ball, 1);
        manager.AddHit(ball);
    }
}
