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

    public List<UIChallengeRow> bumperChallenges;
    public List<UIChallengeRow> rampChallenges;
    public List<UIChallengeRow> paddleChallenges;

    public Text dailyChallengeCountdown;

    private List<int> chosenDailyChallengeIndexs;
    private DateTime lastRecordedDay;

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
    }

    public void ChooseNewDailyChallenges()
    {
        Debug.Log("Populate New Dailies");
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
        foreach(UIChallengeRow challengeRow in bumperChallenges)
        {
            challengeRow.AddProgress(hitNum);
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
