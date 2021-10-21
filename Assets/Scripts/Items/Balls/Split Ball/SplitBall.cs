using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitBall : Ball
{
    [SerializeField]
    private float skillLength;
    [SerializeField]
    private int numSplitBalls;
    [SerializeField]
    private float ballForce;

    private List<GameObject> splitBalls;

    public override void BallSkill()
    {
        base.BallSkill();
        skillActive = true;
        splitBalls = new List<GameObject>();

        for (int i = 0; i < numSplitBalls; i++)
        {
            GameObject newBall = Instantiate(gameObject);
            newBall.GetComponent<SplitBall>().RemoveSplitBalls();
            newBall.GetComponent<Ball>().theRB.velocity = new Vector2(Random.Range(-ballForce, ballForce), Random.Range(-ballForce, ballForce));
        }

        Invoke(nameof(SetInactiveSkill), skillLength);
    }

    private void RemoveSplitBalls()
    {
        Invoke(nameof(DestroyBall), skillLength);
        
    }

    private void DestroyBall()
    {
        Destroy(gameObject);
    }

    private void SetInactiveSkill()
    {
        skillActive = false;
    }
}
