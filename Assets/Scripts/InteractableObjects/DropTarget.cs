using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropTarget : InteractableObject
{
    private DropTargetManager manager;

    private void Start()
    {
        manager = GetComponentInParent<DropTargetManager>();    
    }

    public override void BallHit(Ball ball, Collision2D collision)
    {
        base.BallHit(ball, collision);
        theCollider.enabled = false;
        manager.TargetHit(ball);
    }

    public void ResetDropTarget()
    {
        theAnimator.SetBool("isHit", false);
        theCollider.enabled = true;
    }
}
