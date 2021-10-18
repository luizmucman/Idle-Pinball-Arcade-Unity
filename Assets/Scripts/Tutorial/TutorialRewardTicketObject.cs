using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialRewardTicketObject : TutorialObject
{
    public Ticket rewardedTicket;

    public override void NextTutorial()
    {
        base.NextTutorial();
        
        PlayerManager.instance.ticketDataList.GetItemData(rewardedTicket.GUID).AddExp(1);

        UIManager.instance.uiTicketManager.CheckUnlockedTickets();
        UIManager.instance.uiShopManager.buyPopup.SetPopup(rewardedTicket);
    }
}
