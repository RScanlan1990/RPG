using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
	public List<Item.ItemType> ItemType = new List<Item.ItemType>();
    public Image Icon;

    public Transform ItemInstantiationTransform;
    private GameObject _itemGraphics;
    [HideInInspector]
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
        var loot = item.Graphics.GetComponent<Loot>();
        _itemGraphics = Instantiate(
            item.Graphics,
            ItemInstantiationTransform.transform.position,
            ItemInstantiationTransform.transform.rotation,
            ItemInstantiationTransform
        );
        _itemGraphics.transform.localPosition = loot.HandOffset;
        _itemGraphics.transform.localRotation = Quaternion.Euler(loot.HandRotationOffset.x, loot.HandRotationOffset.y, loot.HandRotationOffset.z);
        Item = item;
        Icon.sprite = Item.Image;
        Icon.enabled = true;
    }

    public void ClearSlot(Item item)
    {
        Destroy(_itemGraphics);
        Item = null;
        Icon.sprite = null;
        Icon.enabled = false;
    }
}
