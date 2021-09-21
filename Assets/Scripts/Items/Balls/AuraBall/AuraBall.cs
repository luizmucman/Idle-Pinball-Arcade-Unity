using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuraBall : Ball
{
    public List<float> cpsMultiplier;

    public float skillLength;
    public GameObject coinAura;

    public override void BallSkill()
    {
        base.BallSkill();
        StartCoroutine(nameof(SpawnAura));
    }

    private IEnumerator SpawnAura()
    {
        GameObject currAura = Instantiate(coinAura, transform);
        yield return new WaitForSecondsRealtime(skillLength);

        Destroy(currAura);
    }
}
