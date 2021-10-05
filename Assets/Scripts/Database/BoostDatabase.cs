using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BoostDatabase", menuName = "ScriptableObjects/BoostDatabase")]
public class BoostDatabase : ScriptableObject
{

    public List<BoostData> database;

    public BoostData GetBoost(string guid)
    {
        foreach(BoostData boost in database)
        {
            if(boost.boostID.Equals(guid)) {
                return boost;
            }
        }

        return null;
    }

    public void SaveBoostDatabase()
    {
        foreach(BoostData boostData in database)
        {
            boostData.SaveBoostDatabase();
        }
    }

    public void LoadBoostDatabase()
    {
        foreach (BoostData boostData in database)
        {
            boostData.LoadBoostDatabase();
        }
    }
}
