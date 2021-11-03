using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIChallengeManager : MonoBehaviour
{
    [SerializeField] private ChallengeDatabaseSO globalChallenges;
    [SerializeField] private ChallengeDatabaseSO dailyChallenges;

    [SerializeField] private UIChallengeRow challengeRowPrefab;
    [SerializeField] private GameObject globalContainer;
    [SerializeField] private GameObject dailyContainer;
    [SerializeField] private GameObject rewardNotification;

    public List<UIChallengeRow> bumperChallenges;
    public List<UIChallengeRow> rampChallenges;
    public List<UIChallengeRow> paddleChallenges;

    public Text dailyChallengeCountdown;

    private List<int> chosenDailyChallengeIndexs;
    private DateTime lastRecordedDay;

    private int unclaimedRewardCount;

    private void Start()
    {
        chosenDailyChallengeIndexs = new List<int>();
        LoadChallenges();
        PopulateChallenges();
    }

    // Update is called once per frame
    void Update()
    {
        if(lastRecordedDay.Date == DateTime.Now.Date)
        {
            string timeLeft = (DateTime.Now.AddDays(1.0).Date - DateTime.Now).ToString("hh\\:mm\\:ss");
            dailyChallengeCountdown.text = "Time Until Reset: " + timeLeft;
        }
        else
        {
            ChooseNewDailyChallenges();
        }
    }

    public void PopulateChallenges()
    {
        foreach (ChallengeData data in globalChallenges.GetChallengeData())
        {
            UIChallengeRow currChallengeRow = Instantiate(challengeRowPrefab, globalContainer.transform);
            //Instantiate(challengeRowPrefab, globalContainer.transform);
            currChallengeRow.SetChallengeRow(data);
            ChallengeType currType = data.GetChallengeType();
            ClassifyChallengeRow(currChallengeRow, currType);
        }

        if (lastRecordedDay.Date == DateTime.Now.Date)
        {
            PopulateSavedDailyChallenges();
        }
        else
        {
            ChooseNewDailyChallenges();
        }

        UIChallengeRow[] globalChallengeRows = globalContainer.GetComponentsInChildren<UIChallengeRow>();
        UIChallengeRow[] dailyChallengeRows = dailyContainer.GetComponentsInChildren<UIChallengeRow>();

        for(int i = globalChallengeRows.Length - 1; i >= 0; i--)
        {
            UIChallengeRow currRow = globalChallengeRows[i];
            if (currRow.GetStatus() == ChallengeStatus.Claim)
            {
                currRow.transform.SetAsFirstSibling();
            }
            else if(currRow.GetStatus() == ChallengeStatus.Claimed)
            {
                currRow.transform.SetAsLastSibling();
            }
        }

        for (int i = dailyChallengeRows.Length - 1; i >= 0; i--)
        {
            UIChallengeRow currRow = dailyChallengeRows[i];
            if (currRow.GetStatus() == ChallengeStatus.Claim)
            {
                currRow.transform.SetAsFirstSibling();
            }
            else if (currRow.GetStatus() == ChallengeStatus.Claimed)
            {
                currRow.transform.SetAsLastSibling();
            }
        }
    }

    public void ChooseNewDailyChallenges()
    {

        lastRecordedDay = DateTime.Now;
        dailyChallenges.ResetChallenges();
        List<ChallengeData> dailyChallengeList = dailyChallenges.GetChallengeData();
        int totalChallenges = dailyChallenges.GetChallengeData().Count - 1;
        chosenDailyChallengeIndexs = new List<int>();

        for (int i = 0; i < 3; i++)
        {
            bool isDupe = true;
            UIChallengeRow currChallengeRow = Instantiate(challengeRowPrefab, dailyContainer.transform);
            ChallengeType currType = new ChallengeType();

            while (isDupe)
            {
                int chosenIndex = UnityEngine.Random.Range((int)0, totalChallenges);
                if(!chosenDailyChallengeIndexs.Contains(chosenIndex))
                {
                    chosenDailyChallengeIndexs.Add(chosenIndex);
                    ChallengeData data = dailyChallengeList[UnityEngine.Random.Range((int)0, totalChallenges)];
                    currChallengeRow.SetChallengeRow(data);
                    currType = data.GetChallengeType();
                    isDupe = false;
                }
            }

            ClassifyChallengeRow(currChallengeRow, currType);
        }
    }

    public void PopulateSavedDailyChallenges()
    {
        Debug.Log("Populate Saved Dailies");
        List<ChallengeData> dailyChallengeList = dailyChallenges.GetChallengeData();
        foreach (int index in chosenDailyChallengeIndexs)
        {
            ChallengeData data = dailyChallengeList[index];
            UIChallengeRow currChallengeRow = Instantiate(challengeRowPrefab, dailyContainer.transform);
            currChallengeRow.SetChallengeRow(data);
            ChallengeType currType = data.GetChallengeType();
            ClassifyChallengeRow(currChallengeRow, currType);
        }
    }

    public void AddChallengeHit(int hitNum, ChallengeType challengeType)
    {
        if (challengeType.Equals(ChallengeType.BumperHit))
        {
            foreach (UIChallengeRow challengeRow in bumperChallenges)
            {
                challengeRow.AddProgress(hitNum);
            }
        }
        else if (challengeType.Equals(ChallengeType.RampHit))
        {
            foreach (UIChallengeRow challengeRow in rampChallenges)
            {
                challengeRow.AddProgress(hitNum);
            }
        }
        else if (challengeType.Equals(ChallengeType.Paddle))
        {
            foreach (UIChallengeRow challengeRow in paddleChallenges)
            {
                challengeRow.AddProgress(hitNum);
            }
        }

    }

    private void ClassifyChallengeRow(UIChallengeRow currChallengeRow, ChallengeType currType)
    {
        if (currType.Equals(ChallengeType.BumperHit))
        {
            bumperChallenges.Add(currChallengeRow);
        }
        else if (currType.Equals(ChallengeType.RampHit))
        {
            rampChallenges.Add(currChallengeRow);
        }
        else if (currType.Equals(ChallengeType.Paddle))
        {
            paddleChallenges.Add(currChallengeRow);
        }
    }

    public void AddUnclaimedChallenge()
    {
        unclaimedRewardCount++;
        rewardNotification.gameObject.SetActive(true);
    }

    public void RemoveUnclaimedChallenge()
    {
        unclaimedRewardCount--;

        if(unclaimedRewardCount <= 0)
        {
            rewardNotification.gameObject.SetActive(false);
        }
    }

    public void SaveChallenges()
    {
        ES3.Save("challenges-recorded-day", lastRecordedDay);
        ES3.Save("chosen-daily-indexs", chosenDailyChallengeIndexs);
        globalChallenges.SaveDatabase();
        dailyChallenges.SaveDatabase();
    }

    public void LoadChallenges()
    {
        lastRecordedDay = ES3.Load("challenges-recorded-day", new DateTime());
        chosenDailyChallengeIndexs = ES3.Load("chosen-daily-indexs", chosenDailyChallengeIndexs);
        globalChallenges.LoadDatabase();
        dailyChallenges.LoadDatabase();
    }
}
