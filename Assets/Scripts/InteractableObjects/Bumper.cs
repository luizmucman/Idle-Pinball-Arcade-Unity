using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bumper : InteractableObject
{
    private BumperManager manager;

    public float bumperForce;

    public override void Awake()
    {
        base.Awake();
        manager = GetComponentInParent<BumperManager>();
    }

    public override void BallHit(Ball ball, Collision2D collision)
    {
        base.BallHit(ball, collision);
        soundsManager.PlaySound("bumperhit");
        ball.theRB.AddForce(collision.GetContact(0).normal * bumperForce, ForceMode2D.Impulse);
        manager.Payout(ball, PlayerManager.instance.playerTicketBuffs.bumperBuff);
    }
}
