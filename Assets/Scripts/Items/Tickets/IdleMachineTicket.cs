using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleMachineTicket : Ticket
{
    public override void EquipTicket()
    {
        base.EquipTicket();
        PlayerManager.instance.playerTicketBuffs.idleCoinBuff += ticketStats[rank];
    }

    public override void UnequipTicket()
    {
        base.UnequipTicket();
        PlayerManager.instance.playerTicketBuffs.idleCoinBuff -= ticketStats[rank];
    }
}
