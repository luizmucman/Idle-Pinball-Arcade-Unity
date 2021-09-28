using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RolloverManager : ObjectManager
{
    private TargetLight[] lights;
    private int hitCount;

    public override void Awake()
    {
        base.Awake();
        lights = GetComponentsInChildren<TargetLight>();
        hitCount = 0;
    }

    public void AddHit(Ball ball)
    {
        if (hitCount < lights.Length)
        {
            lights[hitCount].PlayLitAnim();
            hitCount++;
            if (hitCount == lights.Length)
            {
                foreach (TargetLight light in lights)
                {
                    light.PlayWinAnim();
                }
                hitCount = 0;
                Payout(ball, upgradeData.jackpotMultiplier);
            }
        }
    }
}
