using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoostDescriptionWindow : MonoBehaviour
{

    public Image boostIcon;
    public Text boostTitle;
    public Text boostDuration;
    public Text boostQty;

    public BoostOwnedContainer boostContainer;

    public void SetBoost(BoostOwnedContainer container)
    {
        boostContainer = container;

        boostIcon.sprite = container.boostImage.sprite;
        boostTitle.text = container.boostData.boostAmt.ToString() + "X INCOME BOOST";
        boostDuration.text = "TIME: " + container.boostLengthText.text;
        boostQty.text = "QTY: " + container.boostData.qtyOwned;
    }

    public void UseBoost()
    {
        if (boostContainer.boostData.qtyOwned > 0)
        {
            boostContainer.boostData.qtyOwned--;

            PlayerManager.instance.UseBoost(boostContainer.boostData);
        }
        if (boostContainer.boostData.qtyOwned <= 0)
        {
            PlayerManager.instance.boostInventory.Remove(boostContainer.boostData);
            Destroy(boostContainer.gameObject);
        }
        ES3.Save("playerBoostInventory", PlayerManager.instance.boostInventory);
        gameObject.SetActive(false);
    }

    public void ExitWindow()
    {
        gameObject.SetActive(false);
    }
}
