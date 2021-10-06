using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetterBumperTicket : Ticket
{
    public override void EquipTicket()
    {
        base.EquipTicket();
        PlayerManager.instance.playerTicketBuffs.bumperBuff += ticketStats[rank];
    }

    public override void UnequipTicket()
    {
        base.EquipTicket();
        PlayerManager.instance.playerTicketBuffs.bumperBuff -= ticketStats[rank];
    }
}
