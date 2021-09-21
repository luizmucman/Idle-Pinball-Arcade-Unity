using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardBallButton : TutorialObject
{
    public Ball rewardedBall;

    public override void NextTutorial()
    {
        base.NextTutorial();
        ItemData ballData = new ItemData();
        ballData.GUID = rewardedBall.GUID;
        ballData.rank = 0;
        PlayerManager.instance.ballInventory.Add(ballData);
        UIManager.instance.uiBallManager.AddNewBallUI(ballData);
        UIManager.instance.uiShopManager.buyPopup.SetPopup(rewardedBall);
    }
}
