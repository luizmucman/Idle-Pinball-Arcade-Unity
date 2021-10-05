using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinMakerTicket : Ticket
{
    public List<float> coinDropMultiplier;

    public override void SetItemData(ItemData item)
    {
        base.SetItemData(item);

        currRankDescription = itemDescription.Replace("{Value}", (coinDropMultiplier[rank] * 100).ToString() + "%");
        nextRankDescription = itemDescription.Replace("{Value}", (coinDropMultiplier[rank + 1] * 100).ToString() + "%");
    }

    public override void EquipTicket()
    {
        base.EquipTicket();
        PlayerManager.instance.playerTicketBuffs.coinDropMultiplier += coinDropMultiplier[rank];
        PlayerManager.instance.playerTicketBuffs.isCoinDrop = true;
    }

    public override void UnequipTicket()
    {
        base.UnequipTicket();
        PlayerManager.instance.playerTicketBuffs.coinDropMultiplier -= coinDropMultiplier[rank];
        PlayerManager.instance.playerTicketBuffs.isCoinDrop = false;
    }
}
