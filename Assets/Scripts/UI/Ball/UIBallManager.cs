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
    private float maxBallCheckCounter;

    // Ball Popup
    public UIBallPopup ballPopup;

    private void OnEnable()
    {
        ballContainers = new List<UIBallContainer>();
    }

    private void Update()
    {
        maxBallCheckCounter += Time.deltaTime;

        if (maxBallCheckCounter >= 1)
        {
            if (currMachine.machineData.GetCoinCount() < currentCost)
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
        currMachine = PlayerManager.instance.playerMachineData.currMachine;

        ResetBallUI();

        foreach (UIBallContainer ballContainer in ballContainers)
        {
            ballContainer.ResetMachine();
        }
        SetBallUpgradeCost();
        equippedBallValueText.text = currMachine.currentBallCount.ToString();
        maxBallValueText.text = currMachine.maxEquippedBalls.ToString();
    }

    public void IncreaseMaxBalls()
    {

        if (currMachine.machineData.GetCoinCount() >= currentCost && currMachine.maxEquippedBalls < 50)
        {
            PlayerManager.instance.RemoveCoins(currentCost);
            currMachine.IncreaseMaxBalls();
            currMachine.ShootNormalBall();
            SetBallUpgradeCost();
            maxBallValueText.text = currMachine.maxEquippedBalls.ToString();
        }


        foreach(UIBallContainer ballContainer in ballContainers)
        {
            ballContainer.CheckIfLimitReached();
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
        foreach (Ball ball in PlayerManager.instance.ballDatabase.database)
        {
                UIBallContainer currBallContainer = Instantiate(ballContainerPrefab, BallContent.transform);

                currBallContainer.SetRow(ball);

                ballContainers.Add(currBallContainer);
        }
        CheckUnlockedBalls();
    }

    public void SetNormalBallCount(int num)
    {
        ballContainers[0].SetNormalBallCount(num);
    }

    public void CheckUnlockedBalls()
    {
        foreach(UIBallContainer ballContainer in ballContainers)
        {
            ballContainer.CheckUnlocked();
        }
    }


}
