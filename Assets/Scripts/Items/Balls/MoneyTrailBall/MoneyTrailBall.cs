using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyTrailBall : Ball
{
    [SerializeField]
    private float spawnLength;
    [SerializeField]
    public Coin coin;
    private bool isSpawning;
    [SerializeField]
    private float spawnCD;
    private float spawnCount;

    private void Start()
    {
        spawnCount = spawnCD;
    }

    private void Update()
    {
        if (isSpawning)
        {
            spawnCount += Time.deltaTime;
            if (spawnCount >= spawnCD)
            {
                Coin newCoin = Instantiate(coin);
                newCoin.transform.position = transform.position;
                spawnCount = 0;
            }
        }
    }

    public override void BallSkill()
    {
        if(!isSpawning)
        {
            base.BallSkill();
            StartSpawning();
        }
    }

    private void StartSpawning()
    {
        isSpawning = true;
        Invoke(nameof(StopFrenzy), spawnLength);
    }

    private void StopFrenzy()
    {
        isSpawning = false;
        ballCDCount = 0;
    }
}
