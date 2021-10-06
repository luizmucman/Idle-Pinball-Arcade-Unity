using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxBallsTicket : Ticket
{
    public override void EquipTicket()
    {
        base.EquipTicket();
        PlayerManager.instance.playerTicketBuffs.maxBalls += (int) ticketStats[rank];
    }

    public override void UnequipTicket()
    {
        base.UnequipTicket();
        PlayerManager.instance.playerTicketBuffs.maxBalls -= (int) ticketStats[rank];
    }
}
