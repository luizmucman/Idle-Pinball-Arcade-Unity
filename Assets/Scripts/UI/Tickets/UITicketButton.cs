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
    [SerializeField] private Text rankText;
    [SerializeField] private Text expText;
    [SerializeField] private Slider expSlider;

    // Ticket Data
    public ItemData ticketData;
    public Ticket ticket;

    public void SetTicket(Ticket currTicket)
    {
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
            rankText.gameObject.SetActive(true);
            expText.gameObject.SetActive(true);

            rankText.text = ticketData.rank.ToString();
            expText.text = ticketData.exp.ToString() + "/" + ticketData.expReqs[ticketData.rank].ToString();
            expSlider.maxValue = ticketData.expReqs[ticketData.rank];
            expSlider.value = ticketData.exp;

            if (ticketData.isEquipped)
            {
                gameObject.transform.SetSiblingIndex(1);
            }
            else
            {
                gameObject.transform.SetAsFirstSibling();
            }

        }
        else
        {
            btn.interactable = false;
            lockedOverlay.SetActive(true);

            rankText.text = "0";
            expText.text = "0/1";
            expSlider.maxValue = 1;
            expSlider.value = 0;
        }
    }
}
