using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ManagerUnlock", menuName = "ScriptableObjects/ManagerUnlockData")]
public class ManagerUnlocks : ScriptableObject
{
    public ManagerUnlockLevels[] unlocks;
}

[System.Serializable]
public class ManagerUnlockLevels
{
    public int level;
    public double multiplier;
}