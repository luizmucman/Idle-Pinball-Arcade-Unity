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

}
