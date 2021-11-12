using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIPlayEventMachineBtn : MonoBehaviour
{

    public void OpenCurrEventMachine()
    {
        MachineData machineData = PlayerManager.instance.currentEventMachineData;

        if (SceneManager.GetSceneByName(machineData.machineGUID) != null)
        {
            PlayerManager.instance.currentMachine.SaveMachine();
            PlayerManager.instance.currentMachine.machineData.isPlaying = false;
            PlayerManager.instance.currMachineData = machineData;
            SceneManager.LoadScene(machineData.machineGUID);
        }
    }
}
