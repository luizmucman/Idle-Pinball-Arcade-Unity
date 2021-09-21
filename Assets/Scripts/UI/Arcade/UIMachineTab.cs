using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMachineTab : MonoBehaviour
{
    public GameObject contentContainer;

    public void TabClicked()
    {
        UIManager.instance.uiMachineManager.TabClicked(this);
    }
}
