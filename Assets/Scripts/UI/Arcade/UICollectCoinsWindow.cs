using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICollectCoinsWindow : MonoBehaviour
{
    // UI Components
    public Text collectCoinAmtTxt;

    [Header("Watch Ad Button")]
    public Button watchAdButton;
    public Text freeWatchAdText;
    public Image watchAdImage;

    [Header("Gem Multiplier Button")]
    public Button gemMultiplierButton;
    public int gemMultiplierCost;

    private ulong totalCoinsCollected;
    private ulong multipliedCoinsCollected;
    private ulong currentMultiplier;

    private AdsManager adsManager;

    private void Start()
    {
        adsManager = PlayerManager.instance.GetComponent<AdsManager>();
    }

    public void SetWindow()
    {
        totalCoinsCollected = 0;
        currentMultiplier = 1;

        watchAdButton.gameObject.SetActive(true);
        gemMultiplierButton.gameObject.SetActive(true);

        if (PlayerManager.instance.isAdFree)
        {
            freeWatchAdText.gameObject.SetActive(true);
            watchAdImage.gameObject.SetActive(false);
        }
        else
        {
            freeWatchAdText.gameObject.SetActive(false);
            watchAdImage.gameObject.SetActive(true);
        }

        foreach (MachineData data in PlayerManager.instance.mainMachines)
        {
            if (data.isUnlocked)
            {
                totalCoinsCollected += data.accumulatedCoins;
            }
        }

        foreach (MachineData data in PlayerManager.instance.eventMachines)
        {
            if (data.isUnlocked)
            {
                totalCoinsCollected += data.accumulatedCoins;
            }
        }

        multipliedCoinsCollected = totalCoinsCollected;

        collectCoinAmtTxt.text = PlayerManager.instance.numFormat.Format(multipliedCoinsCollected);

        watchAdButton.gameObject.SetActive(true);
        gameObject.SetActive(true);
    }

    public void CloseWindow()
    {
        gameObject.SetActive(false);
    }

    public void CollectIdleCoins()
    {
        ResetAccumulatedCoins();

        PlayerManager.instance.AddCoins(multipliedCoinsCollected);

        CloseWindow();
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
        currentMultiplier += 1;
        multipliedCoinsCollected = totalCoinsCollected * currentMultiplier;
        collectCoinAmtTxt.text = PlayerManager.instance.numFormat.Format(multipliedCoinsCollected);

        watchAdButton.gameObject.SetActive(false);
    }

    public void RewardGemMultiplier()
    {
        if (PlayerManager.instance.playerGems >= gemMultiplierCost)
        {
            PlayerManager.instance.playerGems -= gemMultiplierCost;
            currentMultiplier += 2;
            multipliedCoinsCollected = totalCoinsCollected * currentMultiplier;
            collectCoinAmtTxt.text = PlayerManager.instance.numFormat.Format(multipliedCoinsCollected);

            gemMultiplierButton.gameObject.SetActive(false);
        }

    }

    private void ResetAccumulatedCoins()
    {
        foreach (MachineData data in PlayerManager.instance.mainMachines)
        {
            if (data.isUnlocked)
            {
                data.accumulatedCoins = 0;
                data.awayCheckPoint = DateTime.Now;
            }
        }

        foreach (MachineData data in PlayerManager.instance.eventMachines)
        {
            if (data.isUnlocked)
            {
                data.accumulatedCoins = 0;
                data.awayCheckPoint = DateTime.Now;
            }
        }
    }
}