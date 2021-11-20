using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WatchAdButtonFreeGems : WatchAdButton
{
    [SerializeField] private UIWindow window;

    public void WatchAd()
    {
        if (PlayerManager.instance.isAdFree)
        {
            RewardGems();
        }
        else
        {
            PlayerManager.instance.GetComponent<AdsManager>().PlayRewardedAd(RewardGems, "TimedGemGift");
        }
    }

    public void RewardGems()
    {
        UIManager.instance.uiShopManager.BuyGems(25);
        UIManager.instance.ResetGemRewardBtn();
        window.CloseAnim();
    }
}
