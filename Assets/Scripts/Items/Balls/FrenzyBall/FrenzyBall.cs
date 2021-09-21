using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrenzyBall : Ball
{
    public MoneyBag moneyBag;
    private bool isFrenzy;
    [SerializeField]
    private float spawnCD;
    private float spawnCount;

    [SerializeField]
    private float frenzyLength;


    private void Start()
    {
        spawnCount = spawnCD;
    }

    private void Update()
    {
        if(isFrenzy)
        {
            spawnCount += Time.deltaTime;
            if (spawnCount >= spawnCD)
            {
                MoneyBag newMoneyBag = Instantiate(moneyBag);
                newMoneyBag.transform.position = new Vector2(Random.Range(-3f, 3f), 7f);
                spawnCount = 0;
            }
        }
    }

    public override void BallSkill()
    {
        base.BallSkill();
        if(!isFrenzy)
        {
            StartFrenzy();
        }

    }

    private void StartFrenzy()
    {
        isFrenzy = true;
        Invoke(nameof(StopFrenzy), frenzyLength);
    }

    private void StopFrenzy()
    {
        isFrenzy = false;
        ballCDCount = 0;
    }
}
