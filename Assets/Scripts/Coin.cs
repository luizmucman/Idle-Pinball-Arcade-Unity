using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Coin : InteractableObject
{
    public double coinMultiplier;

    private double machineCPS;
    private bool isHit;

    public override void Start()
    {
        base.Start();
        isHit = true;
        Invoke(nameof(Activate), 1f);
    }

    public void SetCPS(double cps)
    {
        machineCPS = cps;
    }

    public override void BallHit(Ball ball, Collider2D collision)
    {
        if (!isHit)
        {
            isHit = true;
            PlayerManager.instance.AddCoins(machineCPS * coinMultiplier);
            theAnimator.Play("isHit");
        }
    }

    public void DestroyBag()
    {
        Destroy(gameObject);
    }

    public void Activate()
    {
        isHit = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            DestroyBag();
        }
    }
}
