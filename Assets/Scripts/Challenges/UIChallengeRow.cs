using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ChallengeStatus { Claimed, InProgress, Claim };

public class UIChallengeRow : MonoBehaviour
{
    private bool hasSet;
    private GameObject claimContainer;
    private GameObject inProgressContainer;
    private GameObject claimedContainer;

    [SerializeField] private Text description;
    [SerializeField] private Text progressText;
    [SerializeField] private Text goalText;
    [SerializeField] private Button claimBtn;
    [SerializeField] private Image rewardIcon;
    [SerializeField] private Text rewardTitle;
    [SerializeField] private Slider progressSlider;
    private ChallengeStatus status;

    [HideInInspector] public ChallengeData challengeData;

    public void SetChallengeRow(ChallengeData data, GameObject claimContainer, GameObject inProgressContainer, GameObject claimedContainer)
    {
        SeasonPassItemSO rewardData = data.GetRewardData();
        this.claimContainer = claimContainer;
        this.inProgressContainer = inProgressContainer;
        this.claimedContainer = claimedContainer;

        challengeData = data;
        string challengeDesc = challengeData.GetDescription();

        description.text = challengeDesc.Replace("{Goal}", challengeData.GetGoal().ToString());
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
        SetClaimedStatus();
        GetComponentInParent<ContentFitterRefresh>().RefreshContentFitters();
    }

    public void SetProgress()
    {
        if(challengeData.GoalReached())
        {
            claimBtn.gameObject.SetActive(true);
            if (challengeData.IsClaimed())
            {
                SetClaimedStatus();
            }
            else
            {
                status = ChallengeStatus.Claim;
                claimBtn.interactable = true;
                claimBtn.GetComponentInChildren<Text>().text = "CLAIM";
                UIManager.instance.uiChallengeManager.AddUnclaimedChallenge();
                gameObject.transform.SetParent(claimContainer.transform);
                
            }
            hasSet = true;
        }
        else
        {
            status = ChallengeStatus.InProgress;
            claimBtn.interactable = false;
            claimBtn.GetComponentInChildren<Text>().text = "IN PROGRESS";
            if(gameObject.transform.parent.gameObject != inProgressContainer)
            {
                gameObject.transform.SetParent(inProgressContainer.transform);
            }
        }

        progressSlider.value = challengeData.GetProgress();
        progressText.text = challengeData.GetProgress().ToString() + "/" + challengeData.GetGoal().ToString();

    }

    private void SetClaimedStatus()
    {
       

        claimBtn.interactable = false;
        claimBtn.GetComponentInChildren<Text>().text = "CLAIMED";
        gameObject.transform.SetAsLastSibling();
        status = ChallengeStatus.Claimed;
        gameObject.transform.SetParent(claimedContainer.transform);

        challengeData.SaveData();
    }

    public void AddProgress(int progressNum)
    {
        if (!hasSet)
        {
            challengeData.AddProgress(progressNum);
            SetProgress();
        }

    }

    public ChallengeStatus GetStatus()
    {
        return status;
    }
}
