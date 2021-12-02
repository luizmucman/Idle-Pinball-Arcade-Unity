using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITicketPopup : MonoBehaviour
{
    [Header("UI Elements")]
    public Image ticketImage;
    public Text title;
    [SerializeField] private Text currRankText;
    [SerializeField] private Text nextRankText;
    [SerializeField] private Text expText;
    [SerializeField] private Slider expSlider;
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
        currRankText.text = ticketButton.ticketData.rank.ToString();
        nextRankText.text = (ticketButton.ticketData.rank + 1).ToString();
        expText.text = ticketButton.ticketData.exp.ToString() + "/" + ticketButton.ticketData.expReqs[ticketButton.ticketData.rank].ToString();
        expSlider.maxValue = ticketButton.ticketData.expReqs[ticketButton.ticketData.rank];
        expSlider.value = ticketButton.ticketData.exp;
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
