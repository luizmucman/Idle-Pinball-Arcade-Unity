using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMachineManager : MonoBehaviour
{
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

    // Start is called before the first frame update
    void Start()
    {
        foreach(MachineData machine in PlayerManager.instance.mainMachines)
        {
            UIMachineButton currMachine = Instantiate(machinePrefab, mainContainer.transform);
            currMachine.SetMachine(machine);
            mainMachineButtons.Add(currMachine);
        }

        foreach (MachineData machine in PlayerManager.instance.eventMachines)
        {
            UIMachineButton currMachine = Instantiate(machinePrefab, eventContainer.transform);
            currMachine.SetMachine(machine);
            eventMachineButtons.Add(currMachine);
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
}
