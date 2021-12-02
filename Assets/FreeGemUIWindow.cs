using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeGemUIWindow : UIWindow
{
    [SerializeField] private WatchAdButtonFreeGems adButton;

    public override void OpenAnim()
    {
        base.OpenAnim();
        adButton.CheckAdFree();
    }
}
