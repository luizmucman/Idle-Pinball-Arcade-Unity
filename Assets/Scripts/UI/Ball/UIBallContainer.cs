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
    [SerializeField] private Text rankText;
    [SerializeField] private Text expText;
    [SerializeField] private Slider expSlider;
    public GameObject lockedOverlay;
    [SerializeField] private GameObject rankContainer;

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
            rankContainer.gameObject.SetActive(false);
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
        UIManager.instance.uiBallManager.CheckUnlockedBalls();
    }

    public void ResetMachine()
    {
        if(itemData.isUnlocked)
        {
            instantiatedBalls = new List<Ball>();
            int ballCount = ball.currMachineBallCount;
            for (int i = 0; i < ballCount; i++)
            {
                instantiatedBalls.Add(PlayerManager.instance.playerMachineData.currMachine.AddResetBall(ball));
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

    private void UpdateExpInfo()
    {
        rankText.text = itemData.rank.ToString();
        expText.text = itemData.exp.ToString() + "/" + itemData.expReqs[itemData.rank].ToString();
        expSlider.maxValue = itemData.expReqs[itemData.rank];
        expSlider.value = itemData.exp;
    }

    public void CheckIfLimitReached()
    {
        MachineManager currMachine = PlayerManager.instance.playerMachineData.currMachine;
        if (ball.currMachineBallCount >= ball.ballStats[itemData.rank].maxBallCount || currMachine.currentBallCount >= currMachine.maxEquippedBalls)
        {
            plusButton.interactable = false;

            if (ball.currMachineBallCount > 0)
            {
                minusButton.interactable = true;
            }
        }
        else if (ball.currMachineBallCount <= 0)
        {
            minusButton.interactable = false;

            if (ball.currMachineBallCount < ball.ballStats[itemData.rank].maxBallCount && currMachine.currentBallCount < currMachine.maxEquippedBalls)
            {
                plusButton.interactable = true;
            }
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
        if (ball.currMachineBallCount < ball.ballStats[itemData.rank].maxBallCount && PlayerManager.instance.playerMachineData.currMachine.currentBallCount < PlayerManager.instance.playerMachineData.currMachine.maxEquippedBalls)
        {
            // Adds instantiated ball to list
            instantiatedBalls.Add(PlayerManager.instance.playerMachineData.currMachine.AddBall(ball));

            UpdateCount();

            ES3.Save(PlayerManager.instance.playerMachineData.currMachine.machineSceneName + ball.GUID + "equipped-count", ball.currMachineBallCount);
        }
    }

    public void RemoveBall()
    {
        if (ball.currMachineBallCount > 0)
        {
            Ball currBall = instantiatedBalls[0];
            instantiatedBalls.Remove(currBall);
            PlayerManager.instance.playerMachineData.currMachine.RemoveBall(currBall);
            UpdateCount();
            ES3.Save(PlayerManager.instance.playerMachineData.currMachine.machineSceneName + ball.GUID + "equipped-count", ball.currMachineBallCount);
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
            ball.SetItemData(itemData);
            
            if (itemData.isUnlocked)
            {
                UpdateExpInfo();
                gameObject.transform.SetSiblingIndex(1);
                lockedOverlay.SetActive(false);
                rankText.gameObject.SetActive(true);
                expText.gameObject.SetActive(true);
                CheckIfLimitReached();
            }
            else
            {
                equippedCount.text = "0";
                lockedOverlay.SetActive(true);
                rankText.gameObject.SetActive(false);
                expText.gameObject.SetActive(false);
            }
        }
        else
        {
            gameObject.transform.SetAsFirstSibling();
            rankText.gameObject.SetActive(false);
            expText.gameObject.SetActive(false);
            rankContainer.gameObject.SetActive(false);
            lockedOverlay.SetActive(false);
        }

    }

    public void SetNormalBallCount(int num)
    {
        equippedCount.text = num.ToString();
    }
}
