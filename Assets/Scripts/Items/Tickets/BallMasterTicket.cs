using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMasterTicket : Ticket
{
    public override void EquipTicket()
    {
        base.EquipTicket();
        PlayerManager.instance.playerTicketBuffs.ballCD += (int) ticketStats[rank];
    }

    public override void UnequipTicket()
    {
        base.UnequipTicket();
        PlayerManager.instance.playerTicketBuffs.ballCD -= (int) ticketStats[rank];
    }
}
