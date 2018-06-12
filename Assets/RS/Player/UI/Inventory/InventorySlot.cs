using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
	public List<Item.ItemType> ItemType = new List<Item.ItemType>();
    public Image Icon;
    public Item Item;

    public bool HaveItem()
    {
        return Item != null;
    }

    public Item GetItem()
    {
        return Item;
    }

    public void AddItem(Item item)
    {
        Item = item;
        Icon.sprite = Item.Image;
        Icon.enabled = true;
    }

    public void ClearSlot(Item item)
    {
        Item = null;
        Icon.sprite = null;
        Icon.enabled = false;
    }
}
