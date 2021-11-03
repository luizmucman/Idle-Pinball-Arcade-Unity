using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBall : Ball
{
    public List<float> speedBoost;

    public override void Start()
    {
        base.Start();
        theRB.mass = (float) 1.0 - speedBoost[rank];
    }

    public override void BallSkill()
    {

    }

    public override void SetBallStats()
    {
        base.SetBallStats();

        currRankDescription = itemDescription.Replace("{Value}", speedBoost[rank].ToString());
        nextRankDescription = itemDescription.Replace("{Value}", speedBoost[rank + 1].ToString());
    }

    public override string GetCurrentRankDesc()
    {
        return (speedBoost[rank] * 100).ToString() + "% faster";
    }

    public override string GetNextRankDesc()
    {
        return (speedBoost[rank + 1] * 100).ToString() + "% faster";
    }
}
