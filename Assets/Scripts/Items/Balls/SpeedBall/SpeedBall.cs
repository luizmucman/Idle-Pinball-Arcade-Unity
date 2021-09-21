using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBall : Ball
{
    public List<float> speedBoost;
    public float skillLength;

    public override void BallSkill()
    {
        base.BallSkill();
        StartCoroutine(nameof(SpeedBoost));
    }

    public IEnumerator SpeedBoost()
    {
        Time.timeScale += speedBoost[rank];
        yield return new WaitForSecondsRealtime(skillLength);
        Time.timeScale -= speedBoost[rank];
    }
}
