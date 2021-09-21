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
            splitBalls.Add(newBall);
            newBall.GetComponent<Ball>().theRB.velocity = new Vector2(Random.Range(-ballForce, ballForce), Random.Range(-ballForce, ballForce));
        }

        Invoke(nameof(RemoveSplitBalls), skillLength);
    }

    private void RemoveSplitBalls()
    {
        skillActive = false;
        foreach(GameObject ball in splitBalls) 
        {
            Destroy(ball);
        }
    }
}
