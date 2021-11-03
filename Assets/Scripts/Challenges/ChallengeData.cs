using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ChallengeType { BumperHit, RampHit, Coins, Paddle, BallHit, TargetHit }

[System.Serializable]
public class ChallengeData
{
    [SerializeField] private int challengeID;
    [SerializeField] private ChallengeType challengeType;
    [SerializeField] private int challengeGoal;
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

    public string GetDescription()
    {
        return description;
    }

    public int GetProgress()
    {
        return challengeProgress;
    }

    public int GetGoal()
    {
        return challengeGoal;
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
        ES3.Save(challengeID + "-progress", challengeProgress);
        ES3.Save(challengeID + "-claimed", rewardClaimed);
    }
    public void LoadData()
    {
        challengeProgress = ES3.Load(challengeID + "-progress", 0);
        rewardClaimed = ES3.Load(challengeID + "-claimed", false);
    }
}
