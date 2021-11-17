using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIShopGemAdBtn : MonoBehaviour
{
    [SerializeField] private int gemReward;
    [SerializeField] private Text btnText;
    [SerializeField] private Button watchAdButton;

    private void Update()
    {
        TimeSpan difference = UIManager.instance.uiShopManager.lastRecordedDay.Date - DateTime.Now;

        if (difference.TotalSeconds > 0)
        {
            watchAdButton.interactable = false;
            string timeLeft = (UIManager.instance.uiShopManager.lastRecordedDay.Date - DateTime.Now).ToString("hh\\:mm\\:ss");
            btnText.text = timeLeft;
        }
        else
        {
            watchAdButton.interactable = true;

            if(PlayerManager.instance.isAdFree)
            {
                btnText.text = "FREE";
            }
            else
            {
                btnText.text = "WATCH AD";
            }
        }
    }

    public void WatchAd()
    {
        if (PlayerManager.instance.isAdFree)
        {
            RewardGems();
        }
        else
        {
            PlayerManager.instance.GetComponent<AdsManager>().PlayRewardedAd(RewardGems, "ShopGemGift");
        }

        DateTime nextReset = DateTime.Now.AddDays(1.0).Date.AddHours(8);

        UIManager.instance.uiShopManager.lastRecordedDay = nextReset;

        ES3.Save("gemDailyNextDate", UIManager.instance.uiShopManager.lastRecordedDay);
    }

    public void RewardGems()
    {
        PlayerManager.instance.AddGems(gemReward);
    }
}
