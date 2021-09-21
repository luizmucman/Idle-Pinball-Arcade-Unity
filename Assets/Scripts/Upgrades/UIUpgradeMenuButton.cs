using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIUpgradeMenuButton : UIMenuButton
{
    public override void ClickWindow()
    {
        base.ClickWindow();
        if (isClicked)
        {
            PlayerManager.instance.currentMachine.EnableUpgradeWindow();
        }
    }
}
