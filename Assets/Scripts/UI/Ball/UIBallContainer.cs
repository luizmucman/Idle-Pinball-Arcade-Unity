using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBallContainer : MonoBehaviour
{
    [Header("Ball Data")]
    // Ball Data
    public ItemData itemData;
    public Ball ball;
    public List<Ball> instantiatedBalls;

    [Header("Connected UI")]
    // Static UI Data
    public Image icon;
    public Text title;
    public Text ballDesc;
    public Text maxEquipped;
    public Text cooldown;
    public GameObject lockedOverlay;

    // Bottom Row
    public Button minusButton;
    public Button plusButton;
    public Text equippedCount;

    public void SetRow(Ball currBall)
    {
        itemData = PlayerManager.instance.ballDataList.GetItemData(currBall.GUID);
        ball = currBall;
        icon.sprite = ball.ballIcon;
        ball.rank = itemData.rank;
        title.text = ball.itemName;
        ballDesc.text = ball.itemDescription;

        CheckUnlocked();
    }

    public void UpdateCount()
    {
        equippedCount.text = PlayerManager.instance.currentMachine.equippedBallCount[ball.ballID].ToString();
        CheckIfLimitReached();
    }

    public void ResetMachine()
    {
        if(itemData.isUnlocked)
        {
            instantiatedBalls = new List<Ball>();
            int ballCount = PlayerManager.instance.currentMachine.equippedBallCount[ball.ballID];
            for (int i = 0; i < ballCount; i++)
            {
                instantiatedBalls.Add(PlayerManager.instance.currentMachine.AddResetBall(ball));
            }
            UpdateCount();
            gameObject.transform.SetAsFirstSibling();
            lockedOverlay.SetActive(false);
        }
        else
        {
            equippedCount.text = "0";
            lockedOverlay.SetActive(true);
        }
        CheckUnlocked();
    }

    public void CheckIfLimitReached()
    {
        MachineManager currMachine = PlayerManager.instance.currentMachine;
        if (currMachine.equippedBallCount[ball.ballID] >= ball.ballStats[itemData.rank].maxBallCount || currMachine.currentBallCount >= currMachine.maxEquippedBalls)
        {
            plusButton.enabled = false;
        }
        else if (PlayerManager.instance.currentMachine.equippedBallCount[ball.ballID] <= 0)
        {
            minusButton.enabled = false;
        }
        else
        {
            plusButton.enabled = true;
            minusButton.enabled = true;
        }
    }

    public void AddBall()
    {
        if (ball.rank != itemData.rank)
        {
            ball.SetItemData(itemData);
        }
        if (PlayerManager.instance.currentMachine.equippedBallCount[ball.ballID] < ball.ballStats[itemData.rank].maxBallCount && PlayerManager.instance.currentMachine.currentBallCount < PlayerManager.instance.currentMachine.maxEquippedBalls)
        {
            // Adds instantiated ball to list
            instantiatedBalls.Add(PlayerManager.instance.currentMachine.AddBall(ball));
            UpdateCount();
        }
    }

    public void RemoveBall()
    {
        if (PlayerManager.instance.currentMachine.equippedBallCount[ball.ballID] > 0)
        {
            Ball currBall = instantiatedBalls[0];
            instantiatedBalls.Remove(currBall);
            PlayerManager.instance.currentMachine.RemoveBall(currBall);
            UpdateCount();
        }
    }

    public void OpenPopup()
    {
        UIManager.instance.uiBallManager.OpenPopup(ball);
    }

    public void CheckUnlocked()
    {
        if (itemData.isUnlocked)
        {
            gameObject.transform.SetAsFirstSibling();
            lockedOverlay.SetActive(false);
        }
        else
        {
            equippedCount.text = "0";
            lockedOverlay.SetActive(true);
        }
    }
}
