using UnityEngine;

public class Inventory : MonoBehaviour {

    public Transform InventoryTransform;
	private InventorySlot[] _inventorySlots;

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
		_inventorySlots = InventoryTransform.GetComponentsInChildren<InventorySlot>();
    }

    private void DoItem(Clickable.ClickReturn clickReturn, Vector3 clickPosition, bool haveUiSelectedItem)
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
        var item = Instantiate(clickReturn.ClickedObject.GetComponent<Loot>().Item);
        AddItem(item);
        Destroy(clickReturn.ClickedObject.gameObject);
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

    public bool DropItem(Item item, Vector3 clickedPosition)
    {
        var heading = clickedPosition - transform.position;
        var distance = heading.magnitude;
        var direction = heading / distance;
        var dropedPosition = transform.position + (direction * 1.0f);
        Instantiate(item.Graphics, new Vector3(dropedPosition.x, transform.position.y, dropedPosition.z), transform.rotation);
        return true;
    }

	public Item SlotClicked(InventorySlot slot, Item item)
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

    public float NumberOfFreeSlots()
    {
        var freeSlots = 0;
        foreach (var slot in _inventorySlots)
        {
            if(slot.Item == null)
            {
                freeSlots++;
            }
        }
        return freeSlots;
    }
}
