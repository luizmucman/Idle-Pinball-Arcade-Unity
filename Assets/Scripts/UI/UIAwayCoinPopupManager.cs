using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIAwayCoinPopupManager : MonoBehaviour
{
    public GameObject IdleCollectPopup;

    public TimeSpan span;
    private double currMultiplier;

    // UI Elements
    public Text awayTime;
    public Text coinAmount;
    public GameObject multiplierBtnContainer;

    [Header("Gem Components")]
    public int gemMultiplierCost;
    public Button gemMultiplierButton;

    [Header("Watch Ad Button")]
    // Watch Ad Components
    public WatchAdButton watchAdButton;

    // Machine Data
    private MachineData currMachine;
    private double collectedCoins;
    private double multipliedCoins;

    // Ads Manager
    private AdsManager adsManager;


    private void Start()
    {
        adsManager = PlayerManager.instance.GetComponent<AdsManager>();
    }

    public void SetMachine(MachineData machine)
    {
        currMachine = machine;
        watchAdButton.gameObject.SetActive(true);
        gemMultiplierButton.gameObject.SetActive(true);

        span =  DateTime.Now - machine.awayCheckPoint;
        currMultiplier = 1;

        watchAdButton.CheckAdFree();

        if (PlayerManager.instance.maxIdleTime > span.TotalHours)
        {
            awayTime.text = "Idle For " + span.Hours.ToString() + " Hrs " + span.Minutes.ToString() + " Mins";
        }
        else
        {
            awayTime.text = "Away Time Maxed Out (" + PlayerManager.instance.maxIdleTime.ToString() + " Hrs)";
        }

        collectedCoins = machine.accumulatedCoins;
        coinAmount.text = DoubleFormatter.Format(collectedCoins) + " coins!";

        IdleCollectPopup.SetActive(true);
        
    }

    public void RewardCoins()
    {
        PlayerManager.instance.AddCoins((double) (currMachine.accumulatedCoins));
    }

    public void AdButtonClicked()
    {
        if (PlayerManager.instance.isAdFree)
        {
            Reward2xAd();
        }
        else
        {
            adsManager.PlayRewardedAd(Reward2xAd);
        }
    }

    public void Reward2xAd()
    {
        currMultiplier += 1;
        multipliedCoins = currMachine.accumulatedCoins * currMultiplier;
        coinAmount.text = DoubleFormatter.Format(multipliedCoins) + " coins!";
        watchAdButton.gameObject.SetActive(false);
    }

    public void BuyGemMultiplierClicked()
    {
        if(PlayerManager.instance.playerGems > gemMultiplierCost)
        {
            PlayerManager.instance.playerGems -= gemMultiplierCost;
            currMultiplier += 2;
            multipliedCoins = currMachine.accumulatedCoins * currMultiplier;
            coinAmount.text = DoubleFormatter.Format(multipliedCoins) + " coins!";
            gemMultiplierButton.gameObject.SetActive(false);
        }
    }

    public void CollectButtonClicked()
    {
        RewardCoins();
        ClosePopup();
    }

    public void HideMultiplierButtons()
    {
        multiplierBtnContainer.SetActive(false);
    }

    public void ClosePopup()
    {
        IdleCollectPopup.SetActive(false);
        currMachine.accumulatedCoins = 0;
        currMultiplier = 1;
    }
}
