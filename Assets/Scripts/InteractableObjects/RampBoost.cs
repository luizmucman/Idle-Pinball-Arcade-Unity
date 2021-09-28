using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RampBoost : InteractableObject
{
    private RampManager manager;

    public GameObject boostDirection;

    public override void Start()
    {
        base.Start();
        manager = GetComponentInParent<RampManager>();
    }

    public override void BallHit(Ball ball, Collider2D collision)
    {
        soundsManager.PlaySound("ramphit");
        ball.gameObject.layer = manager.gameObject.layer;
        ball.GetComponent<SpriteRenderer>().sortingLayerName = manager.ramp.GetComponent<SpriteRenderer>().sortingLayerName;
        ball.GetComponent<SpriteRenderer>().sortingOrder = manager.ramp.GetComponent<SpriteRenderer>().sortingOrder + 1;
        ball.gameObject.transform.position = gameObject.transform.position;
        ball.theRB.velocity = Vector2.zero;
        ball.theRB.AddForce((boostDirection.transform.position - ball.transform.position) * 20, ForceMode2D.Impulse);
        manager.Payout(ball, 1f);
        manager.AddHit(ball);
    }
}
