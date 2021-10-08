using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIUpgradeManager : MonoBehaviour
{
    public GameObject rowContainer;

    public UIUpgradeRow rowPrefab;

    public UIUpgradeQtyBtn oneXButton;
    public UIUpgradeQtyBtn fiveXButton;
    public UIUpgradeQtyBtn tenXButton;
    public UIUpgradeQtyBtn maxButton;

    public List<UIUpgradeRow> upgradeRows;

    public void AddUpgradeRow(ObjectManager manager)
    {
        UIUpgradeRow row = Instantiate(rowPrefab, rowContainer.transform);
        row.SetUpgrade(manager);
        upgradeRows.Add(row);
    }

    private void ResetUpgradeButtons()
    {
        oneXButton.ResetBtnImage();
        fiveXButton.ResetBtnImage();
        tenXButton.ResetBtnImage();
        maxButton.ResetBtnImage();
    }

    public void SetSelectedButton(int amount)
    {
        ResetUpgradeButtons();
        SetCostByAmount(amount);
    }

    private void SetCostByAmount(int amount)
    {
        foreach(UIUpgradeRow row in upgradeRows)
        {
            row.SetCostByAmount(amount);
        }

    }

}
