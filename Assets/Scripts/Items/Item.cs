using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Item : MonoBehaviour
{
    public ItemData itemData;

    [Header("Item Data")]
    public string GUID;
    public Sprite itemIcon;
    public string itemName;
    [TextArea(15, 10)]
    public string itemDescription;
    public string currRankDescription;
    public string nextRankDescription;
    public int rank;
    public ItemType itemType;

    public virtual void SetItemData(ItemData item)
    {
        itemData = item;
        rank = item.rank;
    }

    public virtual void SaveItem()
    {
        itemData.SaveItemData();
    }

    public virtual void LoadItem()
    {
        itemData.LoadItemData();
    }
}

public enum ItemType
{
    Boost,
    Ball,
    Ticket
}
