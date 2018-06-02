using UnityEngine;

public class Equiped : MonoBehaviour {

    public Transform Hand;
    public Transform EquipedTransform;
	private ItemSlot[] _slots;
    

	void Start()
    {
		_slots = EquipedTransform.GetComponentsInChildren<ItemSlot>();
	}

    public Item SlotClicked(ItemSlot slot, Item item)
    {
        var savedItem = slot.Item;
        if (savedItem != null)
        {
            slot.ClearSlot(slot.Item);
            DeEquipItem(slot.Item);
        }
        if (item != null && item.IsEquipable)
        {
            if (DoesSlotTypeContain(slot, item.Type))
            {
                slot.AddItem(item);
                EquipItem(slot, item);
            }
        }
        return savedItem;
    }

    private void DeEquipItem(Item item)
    {
        foreach (var slot in _slots)
        {
            if(slot.Item == item)
            {
                slot.Item = null;
            }
        }
    }

    private void EquipItem(ItemSlot slot, Item item)
	{
        slot.Item = item;
        var instantiated = Instantiate(item.ClickableGameObject, Hand.transform.position, Hand.transform.rotation, Hand);
    }

    public bool HaveToolTypeEquiped(Item.ItemTypes itemType)
    {
        foreach (var slot in _slots)
        {
           if(DoesSlotTypeContain(slot, itemType))
           {
                return true;
           }
        }
        return false;
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
