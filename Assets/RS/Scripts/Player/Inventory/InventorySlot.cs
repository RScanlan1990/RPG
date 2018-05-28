﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
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
        Debug.Log("adding item : " + Item);
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
