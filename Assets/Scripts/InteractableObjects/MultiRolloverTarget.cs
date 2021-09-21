using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiRolloverTarget : InteractableObject
{
    private MultiRolloverManager manager;

    private bool isHit;

    public override void Awake()
    {
        base.Awake();
        manager = GetComponentInParent<MultiRolloverManager>();
    }

    public override void ResetTarget()
    {
        theAnimator.Play("isWin");
        base.ResetTarget();
        Invoke(nameof(ResetRollover), 1f);
    }

    public override void BallHit(Ball ball, Collider2D collision)
    {
        if (!isHit)
        {
            theAnimator.SetBool("isHit", true);
            isHit = true;
            manager.AddHit(ball);
        }

        manager.Payout(ball, 1);
    }

    private void ResetRollover()
    {
        isHit = false;
    }
}
