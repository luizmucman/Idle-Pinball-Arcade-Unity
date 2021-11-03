using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ChallengeStatus { Claimed, InProgress, Claim };

public class UIChallengeRow : MonoBehaviour
{


    [SerializeField] private Text description;
    [SerializeField] private Text progressText;
    [SerializeField] private Text goalText;
    [SerializeField] private Button claimBtn;
    [SerializeField] private Image rewardIcon;
    [SerializeField] private Text rewardTitle;
    [SerializeField] private Slider progressSlider;
    private ChallengeStatus status;

    [HideInInspector] public ChallengeData challengeData;

    public void SetChallengeRow(ChallengeData data)
    {
        SeasonPassItemSO rewardData = data.GetRewardData();
        challengeData = data;

        description.text = challengeData.GetDescription();
        progressText.text = challengeData.GetProgress().ToString() + "/" + challengeData.GetGoal().ToString();
        rewardIcon.sprite = rewardData.rewardIcon;
        rewardTitle.text = rewardData.rewardTitle;
        progressSlider.maxValue = challengeData.GetGoal();
        progressSlider.value = challengeData.GetProgress();

        SetProgress();
    }

    public void ClaimRewardBtn()
    {
        challengeData.CollectReward();
        UIManager.instance.uiChallengeManager.RemoveUnclaimedChallenge();
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
                gameObject.transform.SetAsLastSibling();
                status = ChallengeStatus.Claimed;
            }
            else
            {
                claimBtn.interactable = true;
                claimBtn.GetComponentInChildren<Text>().text = "CLAIM";
                UIManager.instance.uiChallengeManager.AddUnclaimedChallenge();
                gameObject.transform.SetAsFirstSibling();
                status = ChallengeStatus.Claim;
            }
        }
        else
        {
            claimBtn.interactable = false;
            claimBtn.GetComponentInChildren<Text>().text = "IN PROGRESS";
            status = ChallengeStatus.InProgress;
        }

        progressSlider.value = challengeData.GetProgress();
        progressText.text = challengeData.GetProgress().ToString() + "/" + challengeData.GetGoal().ToString();
    }

    public void AddProgress(int progressNum)
    {
        challengeData.AddProgress(progressNum);
        SetProgress();
    }

    public ChallengeStatus GetStatus()
    {
        return status;
    }
}
