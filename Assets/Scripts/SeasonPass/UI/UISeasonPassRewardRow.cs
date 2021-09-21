using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISeasonPassRewardRow : MonoBehaviour
{
    // Premium Reward
    public Image premiumRewardIcon;
    public Button premiumClaimButton;

    // Free Reward
    public Image freeRewardIcon;
    public Button freeClaimButton;

    // Tier Circle
    public Image tierCircle;

    public SeasonPassTier tierData;

    public void SetTierData(SeasonPassTier data)
    {
        tierData = data;
        premiumRewardIcon.sprite = tierData.premiumReward.rewardItem.rewardIcon;
        if(tierData.containsFreeReward)
        {
            freeRewardIcon.sprite = tierData.freeReward.rewardItem.rewardIcon;
        }
        else
        {
            freeRewardIcon.transform.parent.gameObject.SetActive(false);
        }
    }

    public void ClaimPremiumReward()
    {
        if(!tierData.premiumReward.isClaimed)
        {
            tierData.premiumReward.RedeemReward();
            SetLevelReached();
        }
    }

    public void ClaimFreeReward()
    {
        if (!tierData.freeReward.isClaimed)
        {
            tierData.freeReward.RedeemReward();
            SetLevelReached();
        }
    }

    public void SetLevelReached()
    {
        tierCircle.color = Color.white;

        if(PlayerManager.instance.seasonPassData.isPremium)
        {
            premiumRewardIcon.color = Color.white;

            premiumClaimButton.gameObject.SetActive(true);

            if (tierData.premiumReward.isClaimed)
            {
                premiumClaimButton.GetComponentInChildren<Text>().text = "CLAIMED";
                premiumClaimButton.enabled = false;
            }
            else
            {
                premiumClaimButton.GetComponentInChildren<Text>().text = "CLAIM";
                premiumClaimButton.enabled = true;
            }
        }
        else
        {
            premiumRewardIcon.color = Color.gray;

            premiumClaimButton.gameObject.SetActive(false);
        }

        if(tierData.containsFreeReward)
        {
            freeRewardIcon.color = Color.white;
            freeClaimButton.gameObject.SetActive(true);
            if (tierData.freeReward.isClaimed)
            {
                freeClaimButton.GetComponentInChildren<Text>().text = "CLAIMED";
                freeClaimButton.enabled = false;
            }
            else
            {
                freeClaimButton.GetComponentInChildren<Text>().text = "CLAIM";
                freeClaimButton.enabled = true;
            }
        }
    }

    public void SetLevelNotReached()
    {
        tierCircle.color = Color.gray;
        premiumRewardIcon.color = Color.gray;

        premiumClaimButton.GetComponentInChildren<Text>().text = "CLAIM";
        premiumClaimButton.gameObject.SetActive(false);

        if(tierData.containsFreeReward)
        {
            freeRewardIcon.color = Color.gray;
            freeClaimButton.GetComponentInChildren<Text>().text = "CLAIM";
            freeClaimButton.gameObject.SetActive(false);
        }
    }

    public void SetTierLevelText(int level)
    {
        tierCircle.GetComponentInChildren<Text>().text = level.ToString();
    }
}
