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

    private void Start()
    {
        playerManager = PlayerManager.instance;
        ticketDiff = playerManager.ticketSlotCount - playerManager.equippedTickets.Count;
        ticketSlotCostText.text = (playerManager.ticketSlotCount * ticketSlotBaseCost).ToString();

        foreach (ItemData ticketData in playerManager.ticketInventory)
        {
            UITicketButton currTicket = Instantiate(ticketButtonPrefab, ownedList.transform);
            currTicket.SetTicket(ticketData);
        }

        foreach (ItemData ticketData in playerManager.equippedTickets)
        {
            UITicketButton currTicket = Instantiate(ticketButtonPrefab, equippedList.transform);
            currTicket.isEquipped = true;
            currTicket.SetTicket(ticketData);
            currTicket.ticket.EquipTicket();
        }

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
        if (ticketButton.isEquipped)
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
        ticketButton.isEquipped = false;
        ticketButton.transform.SetParent(ownedList.transform);
        AddEmptyTicket();
        ticketDiff = playerManager.ticketSlotCount - playerManager.equippedTickets.Count;
        Invoke(nameof(RefreshUI), 0.1f);
    }

    private void EquipTicket(UITicketButton ticketButton)
    {
        RemoveEmptyTicket();
        ticketButton.ticket.EquipTicket();
        ticketButton.isEquipped = true;
        ticketButton.transform.SetParent(equippedList.transform);

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
}
