using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemData
{
    [Header("Base Item Data")]
    // Item Data
    public string GUID;
    public int rank;
    public List<int> expReqs = new List<int>() {0, 3, 9, 27, 81};
    public int exp;
    public bool isUnlocked;

    // Ticket Equipped
    public bool isEquipped;

    public void AddExp(int addedExp)
    {
        if(rank == 0 && addedExp == 1)
        {
            isUnlocked = true;
            rank++;
        }
        else
        {
            isUnlocked = true;
            int totalExp = addedExp + exp;

            while (totalExp > 0 && rank < 5)
            {
                if (totalExp >= expReqs[rank])
                {
                    totalExp -= expReqs[rank];
                    rank++;
                }
                else
                {
                    exp = totalExp;
                    totalExp = 0;
                }
            }

            if (totalExp > 0)
            {
                PlayerManager.instance.AddGems(totalExp * 18);
            }
        }

        SaveItemData();
    }

    public void SaveItemData()
    {
        ES3.Save(GUID + "-rank", rank);
        ES3.Save(GUID + "-exp", exp);
        ES3.Save(GUID + "-isEquipped", isEquipped);
        ES3.Save(GUID + "-isUnlocked", isUnlocked);
    }

    public void LoadItemData()
    {
        rank = ES3.Load(GUID + "-rank", 0);
        exp = ES3.Load(GUID + "-exp", 0);
        isEquipped = ES3.Load(GUID + "-isEquipped", false);
        isUnlocked = ES3.Load(GUID + "-isUnlocked", false);
    }
}
