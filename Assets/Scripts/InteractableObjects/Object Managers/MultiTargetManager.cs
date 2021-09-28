using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiTargetManager : ObjectManager
{
    private MultiTarget[] multiTargets;

    private int targetHP;

    public override void Awake()
    {
        base.Awake();
        multiTargets = GetComponentsInChildren<MultiTarget>();
        targetHP = multiTargets.Length;
    }

    public void AddHit(Ball ball)
    {
        targetHP--;
        if (targetHP <= 0)
        {
            foreach (MultiTarget target in multiTargets)
            {
                target.Win();
            }
            targetHP = multiTargets.Length;
            Payout(ball, upgradeData.jackpotMultiplier);
        }
    }
}
