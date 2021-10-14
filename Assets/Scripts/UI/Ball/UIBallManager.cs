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
    public double ballBaseCost;
    public float costMultiplier;
    public double currentCost;

    // Ball Popup
    public UIBallPopup ballPopup;

    private void OnEnable()
    {
        ballContainers = new List<UIBallContainer>();
        ResetBallUI();
    }

    private void Update()
    {
        if (PlayerManager.instance.currentMachine.machineData.isCurrentEvent)
        {
            if (PlayerManager.instance.eventCoins < currentCost)
            {
                maxBallUpgradeButton.interactable = false;
            }
            else
            {
                maxBallUpgradeButton.interactable = true;
            }
        }
        else
        {
            if (PlayerManager.instance.playerCoins < currentCost)
            {
                maxBallUpgradeButton.interactable = false;
            }
            else
            {
                maxBallUpgradeButton.interactable = true;
            }
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
        if (PlayerManager.instance.currentMachine.machineData.isCurrentEvent)
        {
            if (PlayerManager.instance.eventCoins >= currentCost && currMachine.maxEquippedBalls < 50)
            {
                PlayerManager.instance.RemoveCoins(currentCost);
                currMachine.maxEquippedBalls++;
                currMachine.ShootNormalBall();
                SetBallUpgradeCost();
                maxBallValueText.text = currMachine.maxEquippedBalls.ToString();
            }
        }
        else
        {
            if (PlayerManager.instance.playerCoins >= currentCost && currMachine.maxEquippedBalls < 50)
            {
                PlayerManager.instance.RemoveCoins(currentCost);
                currMachine.maxEquippedBalls++;
                currMachine.ShootNormalBall();
                SetBallUpgradeCost();
                maxBallValueText.text = currMachine.maxEquippedBalls.ToString();
            }
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

            currentCost = (double)(ballBaseCost * (Mathf.Pow(costMultiplier, currMachine.maxEquippedBalls)));
            maxBallUpgradeCostText.text = DoubleFormatter.Format(currentCost);
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
            Destroy(container.gameObject);
            ballContainers.Remove(container);

        }

        foreach (ItemData ballData in PlayerManager.instance.ballInventory)
        {
            UIBallContainer currBallContainer = Instantiate(ballContainerPrefab, BallContent.transform);

            currBallContainer.SetRow(ballData);

            ballContainers.Add(currBallContainer);
        }
    }

    public void AddNewBallUI(ItemData ballData)
    {
        UIBallContainer currBallContainer = Instantiate(ballContainerPrefab, BallContent.transform);

        currBallContainer.SetRow(ballData);

        ballContainers.Add(currBallContainer);
        currBallContainer.UpdateCount();
    }
}
