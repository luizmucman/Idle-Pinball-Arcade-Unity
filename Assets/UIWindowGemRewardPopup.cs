using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIWindowGemRewardPopup : UIWindow
{
    [SerializeField] private Text titleText;

    public override void OpenAnim()
    {
        base.OpenAnim();
    }

    public void SetTitle(int gems)
    {
        titleText.text = "You got " + gems + " gems!";
    }
}
