using UnityEngine;

public class Equiped : MonoBehaviour {

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
            if(slot.ItemTypes.Contains(item.Type))
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
	}
}
