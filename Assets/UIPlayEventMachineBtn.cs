using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIPlayEventMachineBtn : MonoBehaviour
{

    public void OpenCurrEventMachine()
    {
        MachineData machineData = PlayerManager.instance.playerMachineData.currentEventMachineData;

        if (SceneManager.GetSceneByName(machineData.machineGUID) != null)
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
}
