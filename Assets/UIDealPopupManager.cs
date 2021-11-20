using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UIDealPopupManager : MonoBehaviour
{
    [SerializeField] private UIShopDealPopup dealPopupWindow;
    [SerializeField] private UIShopDealPopup masterPackPopupWindow;

    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerManager.instance.is2xAllIncome)
        {
            DateTime lastDealPopup = ES3.Load("last-deal-popup", new DateTime());
            double difference = (lastDealPopup - DateTime.Now).TotalDays;
            if (difference >= 2.0 && difference <= 1000.0)
            {
                dealPopupWindow.GetComponent<UIWindow>().OpenAnim();
                dealPopupWindow.SetTimer();
                ES3.Save("last-deal-popup", DateTime.Now);
            }
            else if (difference > 1000.0)
            {
                ES3.Save("last-deal-popup", DateTime.Now);
            }
        }
        else if (!PlayerManager.instance.is4xAllIncome)
        {
            DateTime lastDealPopup = ES3.Load("last-deal-popup", new DateTime());
            double difference = (lastDealPopup - DateTime.Now).TotalDays;
            if (difference >= 2.0 && difference <= 1000.0)
            {
                masterPackPopupWindow.GetComponent<UIWindow>().OpenAnim();
                ES3.Save("last-deal-popup", DateTime.Now);
            }
            else if (difference > 1000.0)
            {
                ES3.Save("last-deal-popup", DateTime.Now);
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
