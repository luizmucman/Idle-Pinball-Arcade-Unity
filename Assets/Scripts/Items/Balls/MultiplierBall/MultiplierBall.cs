using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplierBall : Ball
{
    public float skillLength;

    public double multiplier;

    public override void BallSkill()
    {
        base.BallSkill();

        multiplier = 2;
        Invoke(nameof(ResetSkill), skillLength);
    }

    private void ResetSkill()
    {
        multiplier = 1;
    }
}
