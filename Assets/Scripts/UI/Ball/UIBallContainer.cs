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
    public Text outOfText;
    public Text maxEquipped;
    public Text cooldown;
    public GameObject lockedOverlay;

    // Bottom Row
    [SerializeField] private GameObject btnContainer;
    public Button minusButton;
    public Button plusButton;
    public Text equippedCount;

    public void SetRow(Ball currBall)
    {
        ball = currBall;
        icon.sprite = ball.ballIcon;
        title.text = ball.itemName;
        

        if (currBall.GUID.Equals("BA000"))
        {
            outOfText.gameObject.SetActive(false);
            maxEquipped.gameObject.SetActive(false);
            btnContainer.gameObject.SetActive(false);
            ballDesc.text = ball.itemDescription;
        }
        else
        {
            itemData = PlayerManager.instance.ballDataList.GetItemData(currBall.GUID);
            ball.rank = itemData.rank;
            ball.SetBallStats();
            maxEquipped.text = ball.ballStats[ball.rank].maxBallCount.ToString();
            ballDesc.text = ball.currRankDescription;

            CheckUnlocked();
        }
    }

    public void UpdateCount()
    {
        equippedCount.text = ball.currMachineBallCount.ToString();
        CheckIfLimitReached();
    }

    public void ResetMachine()
    {
        if(itemData.isUnlocked)
        {
            instantiatedBalls = new List<Ball>();
            int ballCount = ball.currMachineBallCount;
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
        if (ball.currMachineBallCount >= ball.ballStats[itemData.rank].maxBallCount || currMachine.currentBallCount >= currMachine.maxEquippedBalls)
        {
            plusButton.interactable = false;
        }
        else if (ball.currMachineBallCount <= 0)
        {
            minusButton.interactable = false;
        }
        else
        {
            plusButton.interactable = true;
            minusButton.interactable = true;
        }
    }

    public void AddBall()
    {
        if (ball.rank != itemData.rank)
        {
            ball.SetItemData(itemData);
        }
        if (ball.currMachineBallCount < ball.ballStats[itemData.rank].maxBallCount && PlayerManager.instance.currentMachine.currentBallCount < PlayerManager.instance.currentMachine.maxEquippedBalls)
        {
            Debug.Log("Adding Ball");
            // Adds instantiated ball to list
            instantiatedBalls.Add(PlayerManager.instance.currentMachine.AddBall(ball));
            UpdateCount();
        }
    }

    public void RemoveBall()
    {
        if (ball.currMachineBallCount > 0)
        {
            Debug.Log("Removing Ball");
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
        if(!ball.GUID.Equals("BA000"))
        {
            if (itemData.isUnlocked)
            {
                gameObject.transform.SetSiblingIndex(1);
                lockedOverlay.SetActive(false);
            }
            else
            {
                equippedCount.text = "0";
                lockedOverlay.SetActive(true);
            }
        }
        else
        {
            gameObject.transform.SetAsFirstSibling();
            lockedOverlay.SetActive(false);
        }

    }

    public void SetNormalBallCount(int num)
    {
        equippedCount.text = num.ToString();
    }
}
