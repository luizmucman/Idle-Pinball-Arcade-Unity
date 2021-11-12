using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIWindowShop : UIWindow
{
    [SerializeField] private ContentFitterRefresh specialsContentFitter;

    [SerializeField] private GameObject EventMachineProduct;
    [SerializeField] private GameObject StarterPackProduct;
    [SerializeField] private GameObject MasterPackProduct;
    [SerializeField] private GameObject AdFreeProduct;

    public override void OpenAnim()
    {
        base.OpenAnim();
        if(PlayerManager.instance.currentEventMachineData.isUnlocked)
        {
            EventMachineProduct.SetActive(false);
        }
        else
        {
            EventMachineProduct.SetActive(true);
        }

        if (PlayerManager.instance.is2xAllIncome)
        {
            StarterPackProduct.SetActive(false);
        }

        if (PlayerManager.instance.is4xAllIncome)
        {
            MasterPackProduct.SetActive(false);
        }

        if(PlayerManager.instance.isAdFree)
        {
            AdFreeProduct.SetActive(false);
        }

        specialsContentFitter.RefreshContentFitters();
    }
}
