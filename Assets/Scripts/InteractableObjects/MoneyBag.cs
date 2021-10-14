using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyBag : InteractableObject
{
    [SerializeField]
    private double moneyBagMultiplier;
    [SerializeField]
    private float fallSpeed;

    private double machineCPS;
    private bool isHit;

    private void Update()
    {
        transform.position = new Vector2(transform.position.x, transform.position.y - (fallSpeed * Time.deltaTime));
    }

    public void SetCPS(double cps)
    {
        machineCPS = cps;
    }

    public override void BallHit(Ball ball, Collider2D collision)
    {
        if(!isHit)
        {
            isHit = true;
            PlayerManager.instance.AddCoins(machineCPS * moneyBagMultiplier);
            theAnimator.Play("isHit");
        }
    }

    public void DestroyBag()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 6)
        {
            DestroyBag();
        }
    }
}
