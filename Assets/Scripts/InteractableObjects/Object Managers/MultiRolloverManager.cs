using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiRolloverManager : ObjectManager
{
    private int targetHitCount;
    private MultiRolloverTarget[] targets;

    public override void Awake()
    {
        base.Awake();
        targets = GetComponentsInChildren<MultiRolloverTarget>();
    }

    public void AddHit(Ball ball)
    {
        targetHitCount++;
        if(targetHitCount >= targets.Length)
        {
            Payout(ball, jackpotMultiplier);
            targetHitCount = 0;
            foreach(MultiRolloverTarget target in targets)
            {
                target.ResetTarget();
            }
        }
    }
}
