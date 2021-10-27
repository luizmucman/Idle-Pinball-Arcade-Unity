using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleManager : ObjectManager
{

    public override void Start()
    {
        base.Start();
        challengeType = ChallengeType.Paddle;
        hasChallenge = true;
    }

}
