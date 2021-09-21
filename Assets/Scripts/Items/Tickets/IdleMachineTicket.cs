using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleMachineTicket : Ticket
{
    public List<float> idleMultipliers;

    public override void SetItemData(ItemData item)
    {
        base.SetItemData(item);
        
        currRankDescription = itemDescription.Replace("(Value)", (idleMultipliers[rank] * 100).ToString() + "%");
        nextRankDescription = itemDescription.Replace("(Value)", (idleMultipliers[rank + 1] * 100).ToString() + "%");

    }

    public override void EquipTicket()
    {
        base.EquipTicket();
        PlayerManager.instance.playerTicketBuffs.idleCoinBuff += idleMultipliers[rank];
    }

    public override void UnequipTicket()
    {
        base.UnequipTicket();
        PlayerManager.instance.playerTicketBuffs.idleCoinBuff -= idleMultipliers[rank];
    }
}
