using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveEarnerTicket : Ticket
{
    public override void EquipTicket()
    {
        base.EquipTicket();
        PlayerManager.instance.playerTicketBuffs.activeBuff += ticketStats[rank];
    }

    public override void UnequipTicket()
    {
        base.UnequipTicket();
        PlayerManager.instance.playerTicketBuffs.activeBuff -= ticketStats[rank];
    }
}
