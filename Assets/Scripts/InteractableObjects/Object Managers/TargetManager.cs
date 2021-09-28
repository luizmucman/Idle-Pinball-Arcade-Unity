using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetManager : ObjectManager
{
    private TargetLight[] targetLights;
    private int hitCount;

    public override void Awake()
    {
        base.Awake();
        targetLights = GetComponentsInChildren<TargetLight>();
        hitCount = 0;
    }

    public void AddHit(Ball ball)
    {
        if(hitCount < targetLights.Length)
        {
            targetLights[hitCount].PlayLitAnim();
            hitCount++;
            if (hitCount == targetLights.Length)
            {
                foreach (TargetLight light in targetLights)
                {
                    light.PlayWinAnim();
                }
                hitCount = 0;
                Payout(ball, upgradeData.jackpotMultiplier);
            }
        }
    }
}
