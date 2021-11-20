using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIWindowBoosts : UIWindow
{
    [SerializeField] private BoostForAdButton btn;

    public override void OpenAnim()
    {
        base.OpenAnim();
        btn.CheckAdFree();
    }
}
