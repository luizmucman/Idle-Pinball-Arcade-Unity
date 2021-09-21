using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBallManager : MonoBehaviour
{
    public MachineManager currMachine;

    // Max Ball UI
    public Text maxBallUpgradeCostText;
    public Button maxBallUpgradeButton;

    // Ball UI
    public Text equippedBallValueText;
    public Text maxBallValueText;
    public UIBallContainer ballContainerPrefab;
    public GameObject BallContent;

    private List<UIBallContainer> ballContainers;

    // Max Ball Stats
    public ulong ballBaseCost;
    public float costMultiplier;
    public ulong currentCost;

    // Ball Popup
    public UIBallPopup ballPopup;

    private void OnEnable()
    {
        ballContainers = new List<UIBallContainer>();
        ResetBallUI();
    }

    private void Update()
    {
        if (PlayerManager.instance.playerCoins >= currentCost)
        {
            maxBallUpgradeButton.interactable = true;
        }
        else
        {
            maxBallUpgradeButton.interactable = false;
        }
    }

    public void NewMachine() {
        currMachine = PlayerManager.instance.currentMachine;

        foreach(UIBallContainer ballContainer in ballContainers)
        {
            ballContainer.ResetMachine();
        }
        SetBallUpgradeCost();
        equippedBallValueText.text = currMachine.currentBallCount.ToString();
        maxBallValueText.text = currMachine.maxEquippedBalls.ToString();
    }

    public void IncreaseMaxBalls()
    {
        if(PlayerManager.instance.playerCoins >= currentCost && currMachine.maxEquippedBalls < 50)
        {
            PlayerManager.instance.RemoveCoins(currentCost);
            currMachine.maxEquippedBalls++;
            currMachine.ShootNormalBall();
            SetBallUpgradeCost();
            maxBallValueText.text = currMachine.maxEquippedBalls.ToString();
        }
    }
    
    public void SetBallUpgradeCost()
    {
        if(currMachine.maxEquippedBalls == 50)
        {
            maxBallUpgradeCostText.text = "MAX BALLS REACHED";
            maxBallUpgradeButton.enabled = false;
        }
        else if (currMachine.maxEquippedBalls < 50)
        {
            // Cost Calculations
            float currentLevel = Mathf.Pow(costMultiplier, currMachine.maxEquippedBalls);
            float numToBuy = (Mathf.Pow(costMultiplier, 1) - 1);
            float topNum = currentLevel * numToBuy;
            float bottomNum = costMultiplier - 1;

            currentCost = (ulong)(ballBaseCost * (topNum / bottomNum));
            maxBallUpgradeCostText.text = PlayerManager.instance.numFormat.Format(currentCost);
        }
    }

    public void OpenPopup(Ball ball)
    {
        ballPopup.SetPopupData(ball);
        ballPopup.gameObject.SetActive(true);
    }

    public void ResetBallUI()
    {
        foreach(UIBallContainer container in ballContainers)
        {
            ballContainers.Remove(container);
            Destroy(container.gameObject);
        }

        foreach (ItemData ballData in PlayerManager.instance.ballInventory)
        {
            UIBallContainer currBallContainer = Instantiate(ballContainerPrefab, BallContent.transform);

            currBallContainer.SetRow(ballData);
            ballContainers.Add(currBallContainer);
        }
    }
}
