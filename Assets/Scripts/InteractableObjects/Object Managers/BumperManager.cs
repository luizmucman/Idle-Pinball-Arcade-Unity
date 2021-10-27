using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BumperManager : ObjectManager
{

    public override void Start()
    {
        base.Start();
        challengeType = ChallengeType.BumperHit;
        hasChallenge = true;
    }
}
