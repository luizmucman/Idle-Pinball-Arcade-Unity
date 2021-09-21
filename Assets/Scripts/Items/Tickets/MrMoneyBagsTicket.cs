using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MrMoneyBagsTicket : Ticket
{
    public List<float> moneyBagMultiplier;

    public override void SetItemData(ItemData item)
    {
        base.SetItemData(item);

        currRankDescription = itemDescription.Replace("(Value)", (moneyBagMultiplier[rank] * 100).ToString() + "%");
        nextRankDescription = itemDescription.Replace("(Value)", (moneyBagMultiplier[rank + 1] * 100).ToString() + "%");
    }

    public override void EquipTicket()
    {
        base.EquipTicket();
        PlayerManager.instance.playerTicketBuffs.moneyBagMultiplier += moneyBagMultiplier[rank];
        PlayerManager.instance.playerTicketBuffs.isMoneyBag = true;
    }

    public override void UnequipTicket()
    {
        base.UnequipTicket();
        PlayerManager.instance.playerTicketBuffs.moneyBagMultiplier -= moneyBagMultiplier[rank];
        PlayerManager.instance.playerTicketBuffs.isMoneyBag = false;
    }
}
