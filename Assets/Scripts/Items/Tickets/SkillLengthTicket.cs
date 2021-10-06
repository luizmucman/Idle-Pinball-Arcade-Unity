using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillLengthTicket : Ticket
{
    public override void EquipTicket()
    {
        base.EquipTicket();
        PlayerManager.instance.playerTicketBuffs.maxIdleTimeLength += ticketStats[rank];
    }

    public override void UnequipTicket()
    {
        base.UnequipTicket();
        PlayerManager.instance.playerTicketBuffs.maxIdleTimeLength -= ticketStats[rank];
    }
}
