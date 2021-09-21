using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetterBumperTicket : Ticket
{
    public List<float> bumperMultiplier;

    public override void SetItemData(ItemData item)
    {
        base.SetItemData(item);

        currRankDescription = itemDescription.Replace("(Value)", (bumperMultiplier[rank] * 100).ToString() + "%");
        nextRankDescription = itemDescription.Replace("(Value)", (bumperMultiplier[rank + 1] * 100).ToString() + "%");
    }

    public override void EquipTicket()
    {
        base.EquipTicket();
        PlayerManager.instance.playerTicketBuffs.bumperMultiplier += bumperMultiplier[rank];
    }

    public override void UnequipTicket()
    {
        base.EquipTicket();
        PlayerManager.instance.playerTicketBuffs.bumperMultiplier -= bumperMultiplier[rank];
    }
}
