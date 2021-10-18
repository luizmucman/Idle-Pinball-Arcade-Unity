using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemDataList
{
    public List<ItemData> itemList = new List<ItemData>();

    public ItemData GetItemData(string GUID)
    {
        foreach(ItemData data in itemList)
        {
            if (data.GUID.Equals(GUID))
            {
                return data;
            }
        }
        return null;
    }

    public void AddNewItemData(string GUID)
    {
        ItemData newData = new ItemData();
        newData.GUID = GUID;
        newData.exp = 0;
        newData.rank = 0;
        newData.isEquipped = false;

        itemList.Add(newData);
    }

    public void SaveItemList()
    {
        foreach (ItemData data in itemList)
        {
            data.SaveItemData();
        }
    }

    public void LoadItemList()
    {
        foreach (ItemData data in itemList)
        {
            data.LoadItemData();
        }
    }
}
