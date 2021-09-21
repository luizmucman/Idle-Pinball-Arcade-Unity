using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureBall : Ball
{
    public GameObject treasureChest;

    public override void BallSkill()
    {
        base.BallSkill();
        Instantiate(treasureChest, transform);
    }
}
