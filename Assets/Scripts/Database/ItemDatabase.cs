using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Database", menuName = "ScriptableObjects/Database")]
public class ItemDatabase : ScriptableObject
{

    public List<Item> database;

    public Item GetItem(string guid)
    {
        foreach(Item item in database)
        {
            if(item.GUID.Equals(guid)) {
                return item;
            }
        }

        return null;
    }
}
