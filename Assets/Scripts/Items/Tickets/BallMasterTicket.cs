using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMasterTicket : Ticket
{
    public List<int> ballCD;

    public override void SetItemData(ItemData item)
    {
        base.SetItemData(item);

        currRankDescription = itemDescription.Replace("{Value}", (ballCD[rank]).ToString());
        nextRankDescription = itemDescription.Replace("{Value}", (ballCD[rank + 1]).ToString());

    }

    public override void EquipTicket()
    {
        base.EquipTicket();
        PlayerManager.instance.playerTicketBuffs.ballCD += ballCD[rank];
    }

    public override void UnequipTicket()
    {
        base.UnequipTicket();
        PlayerManager.instance.playerTicketBuffs.ballCD -= ballCD[rank];
    }
}
