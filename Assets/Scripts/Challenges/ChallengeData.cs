using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ChallengeType { BumperHit, RampHit, Coins, Paddle, BallHit }

[System.Serializable]
public class ChallengeData
{
    [SerializeField] private ChallengeType challengeType;
    [SerializeField] private int challengeGoal;
    [SerializeField] private string title;
    [SerializeField] private string description;
    [SerializeField] private SeasonPassItemSO reward;
    private int challengeProgress;
    private bool rewardClaimed;

    public void ResetChallengeData()
    {
        rewardClaimed = false;
        challengeProgress = 0;
    }

    public void AddProgress(int progressNum)
    {
        if (challengeProgress < challengeGoal)
        {
            challengeProgress += progressNum;
            if (challengeProgress > challengeGoal)
            {
                challengeProgress = challengeGoal;
            }
        }
    }

    public void CollectReward()
    {
        if (challengeProgress == challengeGoal && !rewardClaimed)
        {
            reward.GetItem();
            rewardClaimed = true;
        }
    }

    public SeasonPassItemSO GetRewardData()
    {
        return reward;
    }

    public string GetTitle()
    {
        return title;
    }

    public string GetDescription()
    {
        return description;
    }

    public string GetProgress()
    {
        return challengeProgress.ToString();
    }

    public string GetGoal()
    {
        return challengeGoal.ToString();
    }
    
    public bool IsClaimed()
    {
        return rewardClaimed;
    }

    public bool GoalReached()
    {
        return (challengeProgress == challengeGoal);
    }

    public ChallengeType GetChallengeType()
    {
        return challengeType;
    }

    public void SaveData()
    {
        Debug.Log("Challenge Saved");
        ES3.Save(title + "-progress", challengeProgress);
        ES3.Save(title + "-claimed", rewardClaimed);
    }
    public void LoadData()
    {
        challengeProgress = ES3.Load(title + "-progress", 0);
        rewardClaimed = ES3.Load(title + "-claimed", false);
    }
}
