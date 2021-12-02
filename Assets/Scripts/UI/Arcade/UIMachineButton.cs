using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIMachineButton : MonoBehaviour
{
    public MachineData machineData;

    [Header("Component References")]
    public Text machineName;
    public Text machineCoins;
    public Image machineCoinsImage;
    public Button machineButton;
    public Button costButton;
    public Image costIcon;
    [SerializeField] private GameObject ownedCoinContainer;


    [Header("Asset References")]
    public Sprite coinIcon;
    public Sprite gemIcon;
    

    private float checkAffordCounter;

    private void Update()
    {
        checkAffordCounter += Time.deltaTime;

        if (checkAffordCounter >= 1f)
        {
            CheckAfford();
        }

    }

    public void SetMachine(MachineData data)
    {
        machineData = data;
        machineName.text = data.machineName;
        costButton.GetComponentInChildren<Text>().text = DoubleFormatter.Format(machineData.machineCost);
        machineButton.image.sprite = machineData.machineImage;
        machineCoinsImage.sprite = machineData.machineCoinImage;

        if (data.isUnlocked || data.isCurrentEvent)
        {
            machineButton.interactable = true;
            costButton.gameObject.SetActive(false);
            ownedCoinContainer.SetActive(true);
            machineCoins.text = DoubleFormatter.Format(data.GetCoinCount());
        }
        else
        {
            machineButton.interactable = false;
            costButton.gameObject.SetActive(true);
            ownedCoinContainer.SetActive(false);
        }

        if(machineData.isEvent)
        {
            costIcon.sprite = gemIcon;
        }
        else
        {
            costIcon.sprite = coinIcon;
        }
    }

    public void LoadMachine()
    {
        if(SceneManager.GetSceneByName(machineData.machineGUID) != null)
        {
            MachineManager currMachine = PlayerManager.instance.playerMachineData.currMachine;
            currMachine.machineData.isPlaying = false;
            ES3.Save(currMachine.machineData.machineGUID + "-isPlaying", false);
            currMachine.SaveMachine();
            currMachine.SetAwayTime();
            PlayerManager.instance.playerMachineData.currMachineData = machineData;
            SceneManager.LoadScene(machineData.machineGUID);
        }

    }

    public void BuyMachine()
    {
        if (machineData.isEvent)
        {
            PlayerManager.instance.RemoveGems((int) machineData.machineCost);
        }
        else
        {
            PlayerManager.instance.playerMachineData.SpendTotalPlayerCoins(machineData.machineCost);
        }

        UnlockMachine();
    }

    public void UnlockMachine()
    {
        machineData.isUnlocked = true;
        ES3.Save(machineData.machineGUID + "-isUnlocked", true);
        SetMachine(machineData);
    }

    private void CheckAfford()
    {
        if (costButton.isActiveAndEnabled)
        {
            if (machineData.isEvent)
            {

                if (PlayerManager.instance.playerGems >= (int)machineData.machineCost)
                {
                    costButton.interactable = true;
                }
                else
                {
                    costButton.interactable = false;
                }
            }
            else
            {
                if (PlayerManager.instance.playerMachineData.GetTotalPlayerCoins() >= machineData.machineCost)
                {
                    costButton.interactable = true;
                }
                else
                {
                    costButton.interactable = false;
                }
            }
        }
        else
        {
            machineCoins.text = DoubleFormatter.Format(machineData.GetCoinCount());
        }
    }
}
