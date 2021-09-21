using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialRewardTicketObject : TutorialObject
{
    public Ticket rewardedTicket;

    public override void NextTutorial()
    {
        base.NextTutorial();
        ItemData ticketData = new ItemData();
        ticketData.GUID = rewardedTicket.GUID;
        ticketData.rank = 0;
        PlayerManager.instance.ballInventory.Add(ticketData);
        UIManager.instance.uiTicketManager.AddNewTicketOwned(ticketData);
        UIManager.instance.uiShopManager.buyPopup.SetPopup(rewardedTicket);
    }
}
