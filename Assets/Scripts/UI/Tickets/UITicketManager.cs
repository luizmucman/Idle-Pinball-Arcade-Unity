using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITicketManager : MonoBehaviour
{
    public PlayerManager playerManager;

    public int ticketDiff;

    public GameObject ownedList;
    public GameObject equippedList;
    public GameObject emptyTicketList;

    public List<GameObject> emptyTicketTracker;
    public GameObject emptyTicketPrefab;
    public UITicketButton ticketButtonPrefab;
    public UITicketPopup ticketPopup;

    public int ticketSlotBaseCost;
    public Text ticketSlotCostText;

    private List<UITicketButton> unEquippedTickets = new List<UITicketButton>();

    private void Start()
    {
        playerManager = PlayerManager.instance;
        SetNewTicketUI();
    }

    private void SetNewTicketUI()
    {
        int ticketsEquipped = 0;
        
        ticketSlotCostText.text = (playerManager.ticketSlotCount * ticketSlotBaseCost).ToString();

        foreach (Ticket ticket in playerManager.ticketDatabase.database)
        {
            if (playerManager.ticketDataList.GetItemData(ticket.GUID).isEquipped)
            {
                UITicketButton currTicket = Instantiate(ticketButtonPrefab, equippedList.transform);
                currTicket.SetTicket(ticket);
                currTicket.ticket.EquipTicket();
                currTicket.CheckUnlocked();

                ticketsEquipped++;
            }
            else
            {
                UITicketButton currTicket = Instantiate(ticketButtonPrefab, ownedList.transform);
                unEquippedTickets.Add(currTicket);
                currTicket.SetTicket(ticket);
                currTicket.CheckUnlocked();
            }
        }

        ticketDiff = playerManager.ticketSlotCount - ticketsEquipped;

        for (int i = 0; i < ticketDiff; i++)
        {
            AddEmptyTicket();
        }
    }

    public void TicketPopup(UITicketButton ticketButton)
    {
        ticketPopup.gameObject.SetActive(true);
        ticketPopup.SetTicket(ticketButton);
    }

    public void TicketInteract(UITicketButton ticketButton)
    {
        if (ticketButton.ticket.itemData.isEquipped)
        {
            UnequipTicket(ticketButton);
        }
        else
        {
            EquipTicket(ticketButton);
        }
    }

    public void BuyTicketSlot()
    {
        if(playerManager.playerGems >= playerManager.ticketSlotCount * ticketSlotBaseCost)
        {
            playerManager.RemoveGems(playerManager.ticketSlotCount * ticketSlotBaseCost);
            playerManager.ticketSlotCount++;
            AddEmptyTicket();
            ticketSlotCostText.text = (playerManager.ticketSlotCount * ticketSlotBaseCost).ToString();
            Invoke(nameof(RefreshUI), 0.1f);
        }
    }

    private void UnequipTicket(UITicketButton ticketButton)
    {
        ticketButton.ticket.UnequipTicket();
        ticketButton.ticket.itemData.isEquipped = false;
        ticketButton.transform.SetParent(ownedList.transform);
        ticketButton.transform.SetAsFirstSibling();
        AddEmptyTicket();
        ticketDiff = playerManager.ticketSlotCount - playerManager.equippedTickets.Count;
        Invoke(nameof(RefreshUI), 0.1f);
    }

    private void EquipTicket(UITicketButton ticketButton)
    {
        RemoveEmptyTicket();
        ticketButton.ticket.EquipTicket();
        ticketButton.ticket.itemData.isEquipped = true;
        ticketButton.transform.SetParent(equippedList.transform);
        ticketButton.gameObject.transform.SetSiblingIndex(1);

        ticketDiff = playerManager.ticketSlotCount - playerManager.equippedTickets.Count;
        Invoke(nameof(RefreshUI), 0.1f);
    }

    private void AddEmptyTicket()
    {
        GameObject currEmptyTicket = Instantiate(emptyTicketPrefab, equippedList.transform);
        emptyTicketTracker.Add(currEmptyTicket);
    }

    private void RemoveEmptyTicket()
    {
        GameObject currEmptyTicket = emptyTicketTracker[0];
        emptyTicketTracker.Remove(currEmptyTicket);
        Destroy(currEmptyTicket);
    }

    private void RefreshUI()
    {
        equippedList.SetActive(false);
        equippedList.SetActive(true);
    }

    public void CheckUnlockedTickets()
    {
        foreach (UITicketButton ticket in unEquippedTickets)
        {
            ticket.CheckUnlocked();
        }
    }
}
