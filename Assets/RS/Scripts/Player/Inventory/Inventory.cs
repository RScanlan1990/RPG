using UnityEngine;

public class Inventory : MonoBehaviour {

    public Transform InventoryTransform;
	private ItemSlot[] _inventorySlots;

    void OnEnable()
    {
        MouseController.OnClick += DoItem;
    }

    void OnDisable()
    {
        MouseController.OnClick -= DoItem;
    }

    void Start()
    {
		_inventorySlots = InventoryTransform.GetComponentsInChildren<ItemSlot>();
    }

    private void DoItem(Clickable.ClickReturn clickReturn, Vector3 clickPosition)
    {
        if (clickReturn != null)
        {
            if (clickReturn.ClickType == Clickable.ClickReturn.ClickTypes.Item)
            {
                switch (clickReturn.ClickAction)
                {
                    case Clickable.ClickReturn.ClickActions.PickUp:
                        PickUpItem(clickReturn);
                        break;
                }
            }
        }
    }

    private void PickUpItem(Clickable.ClickReturn clickReturn)
    {
        var item = clickReturn.ClickedObject.GetComponent<Item>();
        AddItem(item);
        clickReturn.ClickedObject.SetActive(false);
    }

    public void AddItem(Item item)
    {
        if (item != null)
        {
            foreach (var slot in _inventorySlots)
            {
                if (slot.HaveItem() == false)
                {
                    slot.AddItem(item);
                    break;
                }
            }
        }
    }

    public bool DropItem(Item item, Vector3 dropPosition)
    {
        //var heading = dropPosition - transform.position;
        //var distance = heading.magnitude;
        //var direction = heading / distance;
        item.gameObject.transform.parent = null;
        item.gameObject.transform.position = transform.position + (transform.forward * 3.0f);
        return true;
    }

	public Item SlotClicked(ItemSlot slot, Item item)
    {
        var savedItem = slot.Item;
        if (savedItem != null)
        {
            slot.ClearSlot(slot.Item);
        }

        if (item != null)
        {
            slot.AddItem(item);
        }
        return savedItem;
    }
}
