using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldenPaddlesTicket : Ticket
{
    public override void EquipTicket()
    {
        base.EquipTicket();
        PlayerManager.instance.playerTicketBuffs.paddleBuff += ticketStats[rank];
    }

    public override void UnequipTicket()
    {
        base.UnequipTicket();
        PlayerManager.instance.playerTicketBuffs.paddleBuff -= ticketStats[rank];
    }
}
