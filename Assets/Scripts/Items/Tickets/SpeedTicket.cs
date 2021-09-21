using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedTicket : Ticket
{
    public List<float> speedMultipliers;

    public override void SetItemData(ItemData item)
    {
        base.SetItemData(item);

        currRankDescription = itemDescription.Replace("(Value)", (speedMultipliers[rank] * 100).ToString() + "%");
        nextRankDescription = itemDescription.Replace("(Value)", (speedMultipliers[rank + 1] * 100).ToString() + "%");

    }

    public override void EquipTicket()
    {
        PlayerManager.instance.playerTicketBuffs.speedBuff += speedMultipliers[rank];
    }

    public override void UnequipTicket()
    {
        PlayerManager.instance.playerTicketBuffs.speedBuff -= speedMultipliers[rank];
    }
}
