using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGatchaPopup : MonoBehaviour
{
    [SerializeField] private ShopItemType itemType;
    [SerializeField] private UIWindow uiWindow;

    private List<ItemData> itemDataList;

    // UI Components
    [SerializeField] private Image itemIcon;
    [SerializeField] private Text itemTitle;
    [SerializeField] private Text currRankText;
    [SerializeField] private Text nextRankText;
    [SerializeField] private Slider rankSlider;
    [SerializeField] private Text expText;

    public void SetPopup(ItemData itemData)
    {
        Item item;

        if (itemType == ShopItemType.Ball)
        {
            item = PlayerManager.instance.ballDatabase.GetItem(itemData.GUID);
        }
        else if (itemType == ShopItemType.Ticket)
        {
            item = PlayerManager.instance.ticketDatabase.GetItem(itemData.GUID);
        }
        else
        {
            item = null;
        }

        itemIcon.sprite = item.itemIcon;
        itemTitle.text = item.itemName;
        currRankText.text = itemData.rank.ToString();
        nextRankText.text = (itemData.rank + 1).ToString();
        rankSlider.maxValue = itemData.expReqs[itemData.rank];
        rankSlider.value = itemData.exp;
        expText.text = itemData.exp + "/" + itemData.expReqs[itemData.rank];

        uiWindow.OpenAnim();
    }
}
