using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RampExit : InteractableObject
{
    private RampManager manager;

    public override void Awake()
    {
        manager = GetComponentInParent<RampManager>();
    }

    public override void BallHit(Ball ball, Collision2D collision)
    {
        ball.gameObject.layer = 3;
        ball.theRB.velocity = Vector2.zero;
        ball.GetComponent<SpriteRenderer>().sortingLayerName = "Ball";

    }
}
