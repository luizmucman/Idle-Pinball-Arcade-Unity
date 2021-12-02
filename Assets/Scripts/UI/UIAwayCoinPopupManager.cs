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

    private bool currentlyRewarding;


    private void Start()
    {
        adsManager = PlayerManager.instance.GetComponent<AdsManager>();
    }

    public void SetMachine(MachineData machine)
    {
        if(!currentlyRewarding)
        {
            currMachine = machine;
            watchAdButton.gameObject.SetActive(true);
            gemMultiplierButton.gameObject.SetActive(true);
            currentlyRewarding = true;

            TimeSpan idleLimitCheck = DateTime.Now - currMachine.awayCheckPoint;
            double accumulatedCoins;

            if (idleLimitCheck.TotalHours > 2)
            {
                accumulatedCoins = (double)((currMachine.coinsPerSecond * (3600 * (2 + PlayerManager.instance.playerTicketBuffs.maxIdleTimeLength)) * PlayerManager.instance.playerTicketBuffs.idleCoinBuff) * 0.20);
            }
            else
            {
                accumulatedCoins = (double)((currMachine.coinsPerSecond * idleLimitCheck.TotalSeconds * PlayerManager.instance.playerTicketBuffs.idleCoinBuff) * 0.20);
            }

            span = DateTime.Now - machine.awayCheckPoint;
            currMultiplier = 1;

            watchAdButton.CheckAdFree();

            if (PlayerManager.instance.playerMachineData.maxIdleTime > span.TotalHours)
            {
                awayTime.text = "Idle For " + span.Hours.ToString() + " Hrs " + span.Minutes.ToString() + " Mins";
            }
            else
            {
                awayTime.text = "Away Time Maxed Out (" + PlayerManager.instance.playerMachineData.maxIdleTime.ToString() + " Hrs)";
            }

            collectedCoins = accumulatedCoins;
            multipliedCoins = collectedCoins * currMultiplier;
            coinAmount.text = DoubleFormatter.Format(collectedCoins) + " coins!";
        }

        IdleCollectPopup.SetActive(true);
    }

    public void RewardCoins()
    {
        PlayerManager.instance.AddCoins((double) (multipliedCoins));

        currMachine.SaveMachine();
        currentlyRewarding = false;
    }

    public void AdButtonClicked()
    {
        if (PlayerManager.instance.isAdFree)
        {
            Reward2xAd();
        }
        else
        {
            adsManager.PlayRewardedAd(Reward2xAd, "2xIdleCoins");
        }
    }

    public void Reward2xAd()
    {
        currMultiplier += 1;
        multipliedCoins = collectedCoins * currMultiplier;
        coinAmount.text = DoubleFormatter.Format(multipliedCoins) + " coins!";
        watchAdButton.gameObject.SetActive(false);
    }

    public void BuyGemMultiplierClicked()
    {
        if(PlayerManager.instance.playerGems > gemMultiplierCost)
        {
            PlayerManager.instance.playerGems -= gemMultiplierCost;
            currMultiplier += 2;
            multipliedCoins = collectedCoins * currMultiplier;
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
        currMachine.SetAwayCheckpoint();
        currMultiplier = 1;
    }
}
