using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecycleBall : Ball
{
    public List<float> recyclePercent;

    public override void BallSkill()
    {

    }

    public override void SetBallStats()
    {
        base.SetBallStats();

        currRankDescription = itemDescription.Replace("{Value}", recyclePercent[rank].ToString());
        nextRankDescription = itemDescription.Replace("{Value}", recyclePercent[rank + 1].ToString());
    }

    public override string GetCurrentRankDesc()
    {
        return recyclePercent[rank].ToString() + "x coins recycled";
    }

    public override string GetNextRankDesc()
    {
        return recyclePercent[rank + 1].ToString() + "x coins recycled";
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
