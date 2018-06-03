using UnityEngine;

public class Equiped : MonoBehaviour
{

    public Transform Hand;
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

    public GameObject HaveToolTypeEquiped(Item.ItemTypes itemType)
    {
        foreach (var slot in _slots)
        {
           if(DoesSlotTypeContain(slot, itemType))
           {
                return slot.ItemLootInstantiateTransform.GetChild(0).gameObject;
           }
        }
        return null;
    }

    private bool DoesSlotTypeContain(ItemSlot itemSlot, Item.ItemTypes itemType)
    {
        if (itemSlot.ItemType.Contains(itemType))
        {
            return true;
        }
        return false;
    }
}
