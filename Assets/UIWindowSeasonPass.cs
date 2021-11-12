using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIWindowSeasonPass : UIWindow
{
    [SerializeField] private GameObject unlockSeasonPassBtn;

    public override void OpenAnim()
    {
        base.OpenAnim();
        if (PlayerManager.instance.currentEventMachineData.isUnlocked)
        {
            unlockSeasonPassBtn.SetActive(false);
        }
        else
        {
            unlockSeasonPassBtn.SetActive(true);
        }
    }
}
