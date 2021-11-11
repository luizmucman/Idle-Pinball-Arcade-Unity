using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITicketButton : MonoBehaviour
{
    public GameObject lockedOverlay;

    // UI Components
    public Image ticketImage;
    private Button btn;

    // Ticket Data
    public ItemData ticketData;
    public Ticket ticket;

    public void SetTicket(Ticket currTicket)
    {
        ticketImage = GetComponent<Image>();
        ticketData = PlayerManager.instance.ticketDataList.GetItemData(currTicket.GUID);
        ticket = currTicket;
        ticket.SetItemData(ticketData);

        ticketImage.sprite = ticket.image;
        btn = GetComponent<Button>();

        CheckUnlocked();
    }

    public void ButtonClicked()
    {
        UIManager.instance.uiTicketManager.TicketPopup(this);
    }

    public void CheckUnlocked()
    {
        if (ticketData.isUnlocked)
        {
            lockedOverlay.SetActive(false);
            btn.interactable = true;
            gameObject.transform.SetAsFirstSibling();
        }
        else
        {
            btn.interactable = false;
            lockedOverlay.SetActive(true);
        }
    }
}
