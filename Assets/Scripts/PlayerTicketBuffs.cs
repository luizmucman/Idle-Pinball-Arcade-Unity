using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerTicketBuffs 
{
    public float coinBuff;
    public float idleCoinBuff;
    public float speedBuff;
    public int ballCD;
    public float ballSkillLength;
    public int maxBalls;
    public float bumperMultiplier;
    public float cpsMultiplier;

    // Money Bag
    public bool isMoneyBag;
    public float moneyBagMultiplier;

    // Coin Drop
    public bool isCoinDrop;
    public float coinDropMultiplier;
}
