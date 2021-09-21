using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerBall : Ball
{
    public override void BallSkill()
    {
        base.BallSkill();
        foreach(ObjectManager interactableObject in machine.objectManagers)
        {
            
        }
    }
}
