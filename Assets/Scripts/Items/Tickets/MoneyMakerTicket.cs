using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyMakerTicket : Ticket
{
    public override void EquipTicket()
    {
        base.EquipTicket();
        PlayerManager.instance.playerTicketBuffs.coinBuff = ticketStats[rank];
    }

    public override void UnequipTicket()
    {
        base.UnequipTicket();
        PlayerManager.instance.playerTicketBuffs.coinBuff = 1;
    }
}
