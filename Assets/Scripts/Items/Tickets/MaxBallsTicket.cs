using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxBallsTicket : Ticket
{
    public List<int> maxBalls;

    public override void SetItemData(ItemData item)
    {
        base.SetItemData(item);

        currRankDescription = itemDescription.Replace("(Value)", (maxBalls[rank]).ToString());
        nextRankDescription = itemDescription.Replace("(Value)", (maxBalls[rank + 1]).ToString());

    }

    public override void EquipTicket()
    {
        base.EquipTicket();
        PlayerManager.instance.playerTicketBuffs.maxBalls += maxBalls[rank];
    }

    public override void UnequipTicket()
    {
        base.UnequipTicket();
        PlayerManager.instance.playerTicketBuffs.maxBalls -= maxBalls[rank];
    }
}
