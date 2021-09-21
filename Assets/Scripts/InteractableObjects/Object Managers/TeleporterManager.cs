using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleporterManager : ObjectManager
{

    private MachineManager currMachine;

    private List<Ball> ballsTeleported;

    private EntryTeleporter entryTeleporter;
    private ExitTeleporter exitTeleporter;

    public override void Awake()
    {
        base.Awake();
        currMachine = GetComponentInParent<MachineManager>();
        entryTeleporter = GetComponentInChildren<EntryTeleporter>();
        exitTeleporter = GetComponentInChildren<ExitTeleporter>();

        ballsTeleported = new List<Ball>();
    }

    public void TeleportBall(Ball ball, Collider2D collision)
    {
        entryTeleporter.theAnimator.Play("isHit");
        exitTeleporter.theAnimator.Play("isHit");

        ball.gameObject.layer = gameObject.layer;
        ball.GetComponent<SpriteRenderer>().sortingLayerName = exitTeleporter.GetComponent<SpriteRenderer>().sortingLayerName;
        ball.GetComponent<SpriteRenderer>().sortingOrder = exitTeleporter.GetComponent<SpriteRenderer>().sortingOrder + 1;

        foreach(Ball instantiatedBalls in currMachine.instantiatedBalls)
        {
            Physics2D.IgnoreCollision(ball.GetComponent<Collider2D>(), instantiatedBalls.GetComponent<Collider2D>());
        }

        ballsTeleported.Add(ball);

        ball.gameObject.transform.position = exitTeleporter.transform.position;
        Payout(ball, 1);
    }
}
