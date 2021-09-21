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

    public void AddExp(int addedExp)
    {
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
}
