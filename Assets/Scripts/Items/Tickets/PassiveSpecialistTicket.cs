using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveSpecialistTicket : Ticket
{
    public List<float> cpsMultiplier;

    public override void SetItemData(ItemData item)
    {
        base.SetItemData(item);

        currRankDescription = itemDescription.Replace("{Value}", (cpsMultiplier[rank] * 100).ToString() + "%");
        nextRankDescription = itemDescription.Replace("{Value}", (cpsMultiplier[rank + 1] * 100).ToString() + "%");
    }

    public override void EquipTicket()
    {
        base.EquipTicket();
        PlayerManager.instance.playerTicketBuffs.cpsMultiplier += cpsMultiplier[rank];
    }

    public override void UnequipTicket()
    {
        base.UnequipTicket();
        PlayerManager.instance.playerTicketBuffs.cpsMultiplier -= cpsMultiplier[rank];
    }
}
