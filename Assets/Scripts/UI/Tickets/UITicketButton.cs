using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITicketButton : MonoBehaviour
{
    public Image ticketImage;

    public ItemData ticketData;
    public Ticket ticket;

    public bool isEquipped;

    public void SetTicket(ItemData data)
    {
        ticketImage = GetComponent<Image>();
        ticketData = data;
        ticket = (Ticket) PlayerManager.instance.ticketDatabase.GetItem(data.GUID);
        ticket.SetItemData(data);

        ticketImage.sprite = ticket.image;
    }

    public void ButtonClicked()
    {
        UIManager.instance.uiTicketManager.TicketPopup(this);
    }
}
