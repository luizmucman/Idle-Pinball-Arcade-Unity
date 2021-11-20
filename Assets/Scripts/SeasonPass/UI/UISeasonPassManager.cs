using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISeasonPassManager : MonoBehaviour
{
    public GameObject seasonPassButton;
    public GameObject seasonPassWindow;

    // Season Data
    [HideInInspector] public SeasonPassData currSeasonData;
    [HideInInspector] public List<UISeasonPassRewardRow> rewardRows;

    [Header("Window UI Elements")]
    // UI Elements
    public GameObject uiRewardsContainer;
    public Slider expSlider;
    public Text expText;
    public Text currLevelText;
    public Text nextLevelText;

    [Header("Button UI Elements")]
    public Slider btnExpSlider;
    public Text btnExpText;
    public Text btnCurrLevelText;
    public Text btnNextLevelText;

    [Header("Reward Row Prefab")]
    // Prefabs
    public UISeasonPassRewardRow rewardRowPrefab;

    private void Start()
    {
        currSeasonData = PlayerManager.instance.seasonPassData;

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

        currSeasonData.CheckSeasonPassProgress();
        SetCurrLevelUI();
    }

    public void SetCurrLevelUI()
    {
        currLevelText.text = currSeasonData.seasonPassLvl.ToString();
        nextLevelText.text = (currSeasonData.seasonPassLvl + 1).ToString();

        btnCurrLevelText.text = currSeasonData.seasonPassLvl.ToString();
        btnNextLevelText.text = (currSeasonData.seasonPassLvl + 1).ToString();
    }

    public void SetLevelReached(int level)
    {
        rewardRows[level - 1].SetLevelReached();
        currSeasonData.CheckSeasonPassProgress();
        SetCurrLevelUI();
    }

    public void SetExpSlider(double maxValue, double currValue)
    {
        if(currSeasonData.seasonPassLvl < currSeasonData.seasonPassPointReqs.Count)
        {
            expSlider.maxValue = (float) maxValue;
            expSlider.value = (float) currValue;
            expText.text = DoubleFormatter.Format(currValue) + " / " + DoubleFormatter.Format(maxValue);

            btnExpSlider.maxValue = (float)maxValue;
            btnExpSlider.value = (float)currValue;
            btnExpText.text = DoubleFormatter.Format(currValue) + " / " + DoubleFormatter.Format(maxValue);
        }
        else
        {
            expSlider.maxValue = 1;
            expSlider.value = 1;
            expText.text = "MAXED";

            btnExpSlider.maxValue = 1;
            btnExpSlider.value = 1;
            btnExpText.text = "MAXED";
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

    public void HideSeasonPassButton()
    {
        seasonPassButton.gameObject.SetActive(false);
    }

    public void ShowSeasonPassButton()
    {
        seasonPassButton.gameObject.SetActive(true);
    }
}
