using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntryTeleporter : InteractableObject
{
    private TeleporterManager teleporterManager;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        teleporterManager = GetComponentInParent<TeleporterManager>();
    }


    public override void BallHit(Ball ball, Collider2D collision)
    {
        teleporterManager.TeleportBall(ball, collision);
        teleporterManager.Payout(ball, 1f);
    }
}
