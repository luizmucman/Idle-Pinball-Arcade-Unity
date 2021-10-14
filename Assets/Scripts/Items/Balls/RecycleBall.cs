using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecycleBall : Ball
{
    public List<float> recyclePercent;

    public override void BallSkill()
    {

    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
        if(other.gameObject.GetComponent<OutHole>() != null)
        {
            PlayerManager.instance.AddCoins((double) (accumulatedCoins * recyclePercent[rank]));
            accumulatedCoins = 0;
        }
    }
}
