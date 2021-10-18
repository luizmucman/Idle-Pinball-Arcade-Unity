using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITicketPopup : MonoBehaviour
{
    [Header("UI Elements")]
    public Image ticketImage;
    public Text title;
    public Image[] stars;
    public Text currRankDescription;
    public Text nextRankDescription;
    public Button interactButton;

    [Header("Stars")]
    public Sprite litStar;
    public Sprite unlitStar;

    [Header("Unassigned Data")]
    public UITicketButton selectedTicket;

    public void SetTicket(UITicketButton ticketButton)
    {
        ticketImage.sprite = ticketButton.ticketImage.sprite;
        title.text = ticketButton.ticket.itemName;
        for(int i = 0; i < 5; i++)
        {
            if(i <= ticketButton.ticketData.rank)
            {
                stars[i].sprite = litStar;
            }
            else
            {
                stars[i].sprite = unlitStar;
            }
        }
        currRankDescription.text = ticketButton.ticket.currRankDescription;
        nextRankDescription.text = ticketButton.ticket.nextRankDescription;

        if(ticketButton.ticketData.isEquipped)
        {
            interactButton.GetComponentInChildren<Text>().text = "UNEQUIP";
        }
        else
        {
            interactButton.GetComponentInChildren<Text>().text = "EQUIP";
        }

        selectedTicket = ticketButton;
    }

    public void ButtonClicked()
    {
        UIManager.instance.uiTicketManager.TicketInteract(selectedTicket);
        ClosePopup();
    }

    public void ClosePopup()
    {
        this.gameObject.SetActive(false);
    }
}
