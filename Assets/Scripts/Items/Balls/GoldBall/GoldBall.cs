using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldBall : Ball
{
    public float coinTime;

    public override void BallSkill()
    {
        base.BallSkill();
        double coins = (double) (machine.machineData.coinsPerSecond * coinTime);
        PlayerManager.instance.AddCoins(coins);
    }
}
