using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIChallengeRow : MonoBehaviour
{
    [SerializeField] private Text title;
    [SerializeField] private Text description;
    [SerializeField] private Text progressText;
    [SerializeField] private Text goalText;
    [SerializeField] private Button claimBtn;
    [SerializeField] private Image rewardIcon;
    [SerializeField] private Text rewardTitle;

    [HideInInspector] public ChallengeData challengeData;

    public void SetChallengeRow(ChallengeData data)
    {
        SeasonPassItemSO rewardData = data.GetRewardData();
        challengeData = data;

        title.text = challengeData.GetTitle();
        description.text = challengeData.GetDescription();
        progressText.text = challengeData.GetProgress();
        goalText.text = "/" + challengeData.GetGoal();
        rewardIcon.sprite = rewardData.rewardIcon;
        rewardTitle.text = rewardData.rewardTitle;

        SetProgress();
    }

    public void ClaimRewardBtn()
    {
        challengeData.CollectReward();
        SetProgress();
    }

    public void SetProgress()
    {
        if(challengeData.GoalReached())
        {
            claimBtn.gameObject.SetActive(true);
            if (challengeData.IsClaimed())
            {
                claimBtn.interactable = false;
                claimBtn.GetComponentInChildren<Text>().text = "CLAIMED";
            }
            else
            {
                claimBtn.interactable = true;
                claimBtn.GetComponentInChildren<Text>().text = "CLAIM REWARD";
            }
        }
        else
        {
            claimBtn.gameObject.SetActive(false);
        }
        progressText.text = challengeData.GetProgress();
    }

    public void AddProgress(int progressNum)
    {
        challengeData.AddProgress(progressNum);
        SetProgress();
    }
}
