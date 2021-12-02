using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGatchaPopupItem : MonoBehaviour
{
    [SerializeField] private Image itemImage;
    [SerializeField] private Text itemName;

    public void SetItem(Item item) 
    {
        itemImage.sprite = item.itemIcon;
        itemName.text = item.itemName;
    }
}
