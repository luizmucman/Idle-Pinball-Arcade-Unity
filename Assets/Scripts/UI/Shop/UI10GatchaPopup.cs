using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI10GatchaPopup : MonoBehaviour
{
    [SerializeField] private List<UIGatchaPopupItem> gatchaItemList;
    [SerializeField] private UIWindow uiWindow;

    public void SetPopup(ShopItemType itemType, List<ItemData> itemDataList)
    {
        ItemDatabase itemDatabase;

        if (itemType == ShopItemType.Ball)
        {
            itemDatabase = PlayerManager.instance.ballDatabase;
        }
        else if (itemType == ShopItemType.Ticket)
        {
            itemDatabase = PlayerManager.instance.ticketDatabase;
        }
        else {
            itemDatabase = null;
        }

        if (itemDatabase != null)
        {
            for (int i = 0; i < 10; i++)
            {
                Item currItem = itemDatabase.GetItem(itemDataList[i].GUID);
                gatchaItemList[i].SetItem(currItem);
            }

            uiWindow.OpenAnim();
        }
    }
}
