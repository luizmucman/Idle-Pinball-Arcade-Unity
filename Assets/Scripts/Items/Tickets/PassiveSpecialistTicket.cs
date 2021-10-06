using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveSpecialistTicket : Ticket
{
    public override void EquipTicket()
    {
        base.EquipTicket();
        PlayerManager.instance.playerTicketBuffs.cpsBuff += ticketStats[rank];
    }

    public override void UnequipTicket()
    {
        base.UnequipTicket();
        PlayerManager.instance.playerTicketBuffs.cpsBuff -= ticketStats[rank];
    }
}
