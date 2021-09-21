using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Ticket : Item
{
    public Sprite image;

    public virtual void EquipTicket()
    {
        if(!PlayerManager.instance.equippedTickets.Contains(itemData))
        {
            PlayerManager.instance.equippedTickets.Add(itemData);
            PlayerManager.instance.ticketInventory.Remove(itemData);
        }
    }

    public virtual void UnequipTicket()
    {
        PlayerManager.instance.equippedTickets.Remove(itemData);
        PlayerManager.instance.ticketInventory.Add(itemData);
    }
}
