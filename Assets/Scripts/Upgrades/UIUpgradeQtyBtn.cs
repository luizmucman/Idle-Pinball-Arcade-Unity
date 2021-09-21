using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIUpgradeQtyBtn : MonoBehaviour
{
    public int upgradeAmt;

    public void BtnClicked()
    {
        UIManager.instance.uiUpgradeManager.SetSelectedButton(upgradeAmt, GetComponent<Button>());
    }
}
