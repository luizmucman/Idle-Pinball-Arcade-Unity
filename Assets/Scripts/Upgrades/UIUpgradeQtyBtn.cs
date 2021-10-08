using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIUpgradeQtyBtn : MonoBehaviour
{
    public int upgradeAmt;

    public Sprite btnSelected;
    public Sprite btnUnselected;

    public void BtnClicked()
    {
        UIManager.instance.uiUpgradeManager.SetSelectedButton(upgradeAmt);
        GetComponent<Button>().image.sprite = btnSelected;
    }

    public void ResetBtnImage()
    {
        GetComponent<Button>().image.sprite = btnUnselected;
    }
}
 