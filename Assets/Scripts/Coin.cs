using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : InteractableObject
{
    public ulong coinMultiplier;

    private ulong machineCPS;
    private bool isHit;

    private void Start()
    {
        isHit = true;
        Invoke(nameof(Activate), 1f);
    }

    public void SetCPS(ulong cps)
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
