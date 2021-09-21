using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyMakerTicket : Ticket
{
    public List<float> coinMultipliers;

    public override void SetItemData(ItemData item)
    {
        base.SetItemData(item);

        currRankDescription = itemDescription.Replace("(Value)", (coinMultipliers[rank] * 100).ToString() + "%");
        nextRankDescription = itemDescription.Replace("(Value)", (coinMultipliers[rank + 1] * 100).ToString() + "%");

    }

    public override void EquipTicket()
    {
        base.EquipTicket();
        PlayerManager.instance.playerTicketBuffs.coinBuff += coinMultipliers[rank];
    }

    public override void UnequipTicket()
    {
        base.UnequipTicket();
        PlayerManager.instance.playerTicketBuffs.coinBuff -= coinMultipliers[rank];
    }
}
