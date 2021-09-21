using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISeasonPassManager : MonoBehaviour
{
    public GameObject seasonPassWindow;

    // Season Data
    [HideInInspector] public SeasonPassData currSeasonData;
    [HideInInspector] public List<UISeasonPassRewardRow> rewardRows;

    // UI Elements
    public GameObject uiRewardsContainer;
    public Slider expSlider;
    public Text expText;

    // Prefabs
    public UISeasonPassRewardRow rewardRowPrefab;

    private void Start()
    {
        currSeasonData = PlayerManager.instance.seasonPassData;

        SetExpSlider();

        int rewardIndex = 0;
        foreach(SeasonPassTier tier in currSeasonData.seasonPassTiers)
        {
            UISeasonPassRewardRow rewardContainer = Instantiate(rewardRowPrefab, uiRewardsContainer.transform);

            rewardContainer.SetTierData(tier);
            rewardContainer.SetTierLevelText(rewardIndex + 1);
            rewardRows.Add(rewardContainer);
            
            if(currSeasonData.seasonPassLvl >= rewardIndex + 1)
            {
                rewardContainer.SetLevelReached();
            }
            else
            {
                rewardContainer.SetLevelNotReached();
            }

            rewardIndex++;
        }
    }

    public void SetLevelReached(int level)
    {
        rewardRows[level - 1].SetLevelReached();
        SetExpSlider();
    }

    public void SetExpSlider()
    {
        if(currSeasonData.seasonPassLvl < currSeasonData.seasonPassPointReqs.Count)
        {
            expSlider.maxValue = currSeasonData.seasonPassPointReqs[currSeasonData.seasonPassLvl - 1];
            expSlider.value = currSeasonData.seasonPassPoints;
            expText.text = currSeasonData.seasonPassPoints + "/" + currSeasonData.seasonPassPointReqs[currSeasonData.seasonPassLvl - 1];
        }
        else
        {
            expSlider.maxValue = 1;
            expSlider.value = 1;
            expText.text = "MAXED";
        }

    }

    public void OpenSeasonPassWindow()
    {
        seasonPassWindow.SetActive(true);
    }

    public void CloseSeasonPassWindow()
    {
        seasonPassWindow.SetActive(false);
    }
}
