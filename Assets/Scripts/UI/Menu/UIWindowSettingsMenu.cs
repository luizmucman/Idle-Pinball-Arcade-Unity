using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIWindowSettingsMenu : UIWindow
{
    [SerializeField] private Text adsFreeOwnedTxt;
    [SerializeField] private Text starterPackOwnedTxt;
    [SerializeField] private Text masterOwnedTxt;

    public override void OpenAnim()
    {
        base.OpenAnim();
        if(PlayerManager.instance.isAdFree)
        {
            adsFreeOwnedTxt.text = "OWNED";
            adsFreeOwnedTxt.color = Color.green;
        }
        if (PlayerManager.instance.is2xAllIncome)
        {
            starterPackOwnedTxt.text = "OWNED";
            starterPackOwnedTxt.color = Color.green;
        }
        if (PlayerManager.instance.is4xAllIncome)
        {
            masterOwnedTxt.text = "OWNED";
            masterOwnedTxt.color = Color.green;
        }
    }
}
