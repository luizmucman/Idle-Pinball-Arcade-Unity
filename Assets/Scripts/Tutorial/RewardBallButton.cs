using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardBallButton : TutorialObject
{
    public Ball rewardedBall;

    public override void NextTutorial()
    {
        base.NextTutorial();
        PlayerManager.instance.ballDataList.GetItemData(rewardedBall.GUID).AddExp(1);

        UIManager.instance.uiBallManager.CheckUnlockedBalls();
        UIManager.instance.uiShopManager.buyPopup.SetPopup(rewardedBall);
    }
}
