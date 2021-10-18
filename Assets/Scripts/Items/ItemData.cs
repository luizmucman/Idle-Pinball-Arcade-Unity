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
    public int exp;
    public bool isUnlocked;

    // Ticket Equipped
    public bool isEquipped;

    public void AddExp(int addedExp)
    {
        isUnlocked = true;
        for(int i = 0; i < addedExp; i++)
        {
            exp++;

            if(exp == (Mathf.Pow(3, rank + 1) / 3))
            {
                rank++;
                exp = 0;
            }
        }
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
