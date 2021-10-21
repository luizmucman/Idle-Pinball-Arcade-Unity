using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIMachineButton : MonoBehaviour
{
    public MachineData machineData;

    public Text machineName;
    public Button machineButton;
    public Button costButton;
    public Image costIcon;
    public Sprite coinIcon;
    public Sprite gemIcon;

    private void Update()
    {
        CheckAfford();
    }

    public void SetMachine(MachineData data)
    {
        machineData = data;
        machineName.text = data.machineName;
        costButton.GetComponentInChildren<Text>().text = DoubleFormatter.Format(machineData.machineCost);
        machineButton.image.sprite = machineData.machineImage;


        if (data.isUnlocked || data.isCurrentEvent)
        {
            machineButton.interactable = true;
            costButton.gameObject.SetActive(false);

        }
        else
        {
            machineButton.interactable = false;
            costButton.gameObject.SetActive(true);
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
            PlayerManager.instance.currentMachine.SaveMachine();
            PlayerManager.instance.currentMachine.machineData.isPlaying = false;
            PlayerManager.instance.currMachineData = machineData;
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
            PlayerManager.instance.RemoveCoins(machineData.machineCost);
        }

        UnlockMachine();
    }

    public void UnlockMachine()
    {
        machineData.isUnlocked = true;
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
                if (PlayerManager.instance.playerCoins >= machineData.machineCost)
                {
                    costButton.interactable = true;
                }
                else
                {
                    costButton.interactable = false;
                }
            }
        }
    }
}
