using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoostForAdButton : MonoBehaviour
{
    private AdsManager adsManager;

    private Button btn;

    [SerializeField] private Sprite adSprite;
    [SerializeField] private Sprite noAdSprite;

    private void Start()
    {
        adsManager = PlayerManager.instance.GetComponent<AdsManager>();
        btn = GetComponent<Button>();
    }

    private void Update()
    {
        if (PlayerManager.instance.boostDatabase.GetBoost("BOOAD").boostLength > 4)
        {
            btn.interactable = false;
        }
        else
        {
            btn.interactable = true;
        }
    }

    public void WatchAd()
    {
        if (PlayerManager.instance.isAdFree)
        {
            RewardAdBoost();
        }
        else
        {
            adsManager.PlayRewardedAd(RewardAdBoost, "2xTemporaryBoost");
        }
    }

    private void RewardAdBoost()
    {
        BoostData boost = new BoostData();

        boost.boostID = "BOOAD";
        boost.boostLength = 2;

        PlayerManager.instance.UseBoost(boost);
    }

    public void CheckAdFree()
    {
        if (PlayerManager.instance.isAdFree)
        {
            btn.image.sprite = noAdSprite;
        } 
        else
        {
            btn.image.sprite = adSprite;
        }
    }
}
