using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMachineManager : MonoBehaviour
{
    [SerializeField] private Text totalCoinText;

    // Machine Prefab
    public UIMachineButton machinePrefab;

    // UI Containers
    public GameObject mainContainer;
    public GameObject eventContainer;

    // Tab Buttons
    public List<UIMachineTab> tabButtons;

    // MachineButton Lists
    public List<UIMachineButton> mainMachineButtons;
    public List<UIMachineButton> eventMachineButtons;
    public List<UIMachineButton> ownedMachineButtons;


    // Start is called before the first frame update
    void Start()
    {
        foreach(MachineData machine in PlayerManager.instance.playerMachineData.mainMachines)
        {
            UIMachineButton currMachine = Instantiate(machinePrefab, mainContainer.transform);
            currMachine.SetMachine(machine);
            mainMachineButtons.Add(currMachine);
            if (currMachine.machineData.isUnlocked)
            {
                ownedMachineButtons.Add(currMachine);
            }
        }

        UIMachineButton currEvent = null;

        foreach (MachineData machine in PlayerManager.instance.playerMachineData.eventMachines)
        {
            UIMachineButton currMachine = Instantiate(machinePrefab, eventContainer.transform);
            currMachine.SetMachine(machine);
            eventMachineButtons.Add(currMachine);
            if(machine.isUnlocked)
            {
                currMachine.gameObject.transform.SetAsFirstSibling();
            }
            if(machine.isCurrentEvent)
            {
                currEvent = currMachine;
                ownedMachineButtons.Add(currMachine);
            }

            if (machine.isUnlocked)
            {
                ownedMachineButtons.Add(currMachine);
            }
        }

        if(currEvent != null)
        {
            currEvent.gameObject.transform.SetAsFirstSibling();
        }

    }

    public void TabClicked(UIMachineTab tabButton)
    {
        ResetTabs();
        tabButton.GetComponent<Image>().color = Color.white;
        tabButton.contentContainer.SetActive(true);
    }

    public void ResetTabs()
    {
        foreach (UIMachineTab tab in tabButtons)
        {
            tab.GetComponent<Image>().color = Color.gray;
            tab.contentContainer.SetActive(false);
        }
    }

    public void UnlockEventMachine(string machineGUID)
    {
        foreach (UIMachineButton machineButton in eventMachineButtons)
        {
            if (machineButton.machineData.machineGUID.Equals(machineGUID))
            {
                machineButton.UnlockMachine();
                return;
            }
        }
    }

    public void SetTotalCoinText(double num)
    {
        totalCoinText.text = DoubleFormatter.Format(num);
    }
}
