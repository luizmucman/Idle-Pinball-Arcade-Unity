using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Ticket : Item
{
    public Sprite image;

    public List<float> ticketStats;

    public override void SetItemData(ItemData item)
    {
        base.SetItemData(item);

        currRankDescription = itemDescription.Replace("{Value}", (ticketStats[rank]).ToString());
        nextRankDescription = itemDescription.Replace("{Value}", (ticketStats[rank + 1]).ToString());

    }

    public virtual void EquipTicket()
    {
        if(!PlayerManager.instance.equippedTickets.Contains(itemData))
        {
            PlayerManager.instance.equippedTickets.Add(itemData);
        }
    }

    public virtual void UnequipTicket()
    {
        PlayerManager.instance.equippedTickets.Remove(itemData);
    }

    public override void SaveItem()
    {
        base.SaveItem();
    }

    public override void LoadItem()
    {
        base.LoadItem();
    }
}
