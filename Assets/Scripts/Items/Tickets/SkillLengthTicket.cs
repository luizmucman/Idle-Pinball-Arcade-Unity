using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillLengthTicket : Ticket
{
    public List<float> ballSkillLength;

    public override void SetItemData(ItemData item)
    {
        base.SetItemData(item);

        currRankDescription = itemDescription.Replace("(Value)", (ballSkillLength[rank]).ToString());
        nextRankDescription = itemDescription.Replace("(Value)", (ballSkillLength[rank + 1]).ToString());

    }

    public override void EquipTicket()
    {
        base.EquipTicket();
        PlayerManager.instance.playerTicketBuffs.ballSkillLength += ballSkillLength[rank];
    }

    public override void UnequipTicket()
    {
        base.UnequipTicket();
        PlayerManager.instance.playerTicketBuffs.ballSkillLength -= ballSkillLength[rank];
    }
}
