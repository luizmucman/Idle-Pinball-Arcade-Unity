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

    public ChallengeData challengeData;

    public void SetChallengeRow(ChallengeData data)
    {
        challengeData = data;

        title.text = challengeData.GetTitle();
        description.text = challengeData.GetDescription();
        progressText.text = challengeData.GetProgress();
        goalText.text = challengeData.GetGoal();
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
            if(challengeData.IsClaimed())
            {
                claimBtn.interactable = false;
                claimBtn.GetComponentInChildren<Text>().text = "CLAIMED";
            }
            else
            {
                claimBtn.interactable = true;
                claimBtn.GetComponentInChildren<Text>().text = "CLAIM";
            }
        }
        else
        {
            claimBtn.interactable = false;
            claimBtn.GetComponentInChildren<Text>().text = challengeData.GetProgress() + "/" + challengeData.GetGoal();
        }
    }

    public void AddProgress(int progressNum)
    {
        challengeData.AddProgress(progressNum);
        SetProgress();
    }
}
