using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ChallengeDatabase", menuName = "Challenge Database")]

public class ChallengeDatabaseSO : ScriptableObject
{
    [SerializeField] private List<ChallengeData> challengeData;

    public List<ChallengeData> GetChallengeData()
    {
        return challengeData;
    }

    public void ResetChallenges()
    {
        foreach(ChallengeData data in challengeData)
        {
            data.ResetChallengeData();
        }
    }

    public void SaveDatabase()
    {
        foreach(ChallengeData data in challengeData)
        {
            data.SaveData();
        }
    }

    public void LoadDatabase()
    {
        foreach (ChallengeData data in challengeData)
        {
            data.LoadData();
        }
    }


}
