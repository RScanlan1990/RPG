using System.Collections.Generic;
using UnityEngine;

public class Equiped : MonoBehaviour
{
    public Transform EquipedTransform;
	private ItemSlot[] _slots;

    void Start()
    {
		_slots = EquipedTransform.GetComponentsInChildren<ItemSlot>();
	}

    public Item SlotClicked(ItemSlot clickedSlot, Item selectedItem)
    {
        var savedItem = clickedSlot.Item;
        if (savedItem != null)
        {
            DeEquipItem(clickedSlot, clickedSlot.Item);
        }
        if (selectedItem != null && selectedItem.IsEquipable)
        {
            if (DoesSlotTypeContain(clickedSlot, selectedItem.Type))
            {
                EquipItem(clickedSlot, selectedItem);
            }
        }
        return savedItem;
    }

    private void DeEquipItem(ItemSlot slot, Item item)
    {
        slot.ClearSlot(slot.Item);
    }

    private void EquipItem(ItemSlot slot, Item itemToEquip)
	{
        slot.AddItem(itemToEquip);
    }

    public bool HaveItemEquiped(List<Item> items)
    {
        foreach (var item in items)
        {
            foreach (var slot in _slots)
            {
                var slotId = slot.Item == null ? -1 : slot.Item.Id;
                if (item.Id == slotId)
                {
                    return true;
                }
            }
        }
        return false;
    }

    public bool HaveItemEquiped(Item item)
    {
        foreach (var slot in _slots)
        {
            if(slot.Item == item)
            {
                return true;
            }
        }
        return false;
    }

    public GameObject HaveToolTypeEquiped(Item.ItemType itemType)
    {
        foreach (var slot in _slots)
        {
            if(DoesSlotTypeContain(slot, itemType))
            {
                if(slot.ItemInstantiationTransform.childCount > 0)
                {
                    return slot.ItemInstantiationTransform.GetChild(0).gameObject;
                }
            }
        }
        return null;
    }

    private bool DoesSlotTypeContain(ItemSlot itemSlot, Item.ItemType itemType)
    {
        if (itemSlot.ItemType.Contains(itemType))
        {
            return true;
        }
        return false;
    }
}
