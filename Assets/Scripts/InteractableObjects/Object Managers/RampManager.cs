using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RampManager : ObjectManager
{
    private TargetLight[] lights;
    private int hitCount;

    [HideInInspector] public Ramp ramp;

    public override void Awake()
    {
        base.Awake();
        ramp = GetComponentInChildren<Ramp>();
        lights = GetComponentsInChildren<TargetLight>();
        hitCount = 0;
    }

    public void AddHit(Ball ball)
    {
        if (hitCount < lights.Length)
        {
            lights[hitCount].PlayLitAnim();
            ramp.PlayHit();
            hitCount++;
            if (hitCount == lights.Length)
            {
                foreach (TargetLight light in lights)
                {
                    light.PlayWinAnim();
                }
                ramp.PlayWin();
                hitCount = 0;
                Payout(ball, jackpotMultiplier);
            }
        }
    }
}
