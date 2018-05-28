using UnityEngine;

public class Inventory : MonoBehaviour
{
    private Transform _inventory;
    private InventorySlot[] _invetorySlots;

    public void Init(Transform inventory)
    {
        _inventory = inventory;
        _invetorySlots = _inventory.GetComponentsInChildren<InventorySlot>();
    }

    public void DoItem(Clickable.ClickReturn clickReturn)
    {
        switch (clickReturn.ClickAction)
        {
            case Clickable.ClickReturn.ClickActions.PickUp:
                PickUpItem(clickReturn);
                break;
        }
    }

    private void PickUpItem(Clickable.ClickReturn clickReturn)
    {
        var item = clickReturn.ClickedObject.GetComponent<Clickable>().Item;
        AddItem(item);
        Destroy(clickReturn.ClickedObject);
    }

    public void AddItem(Item item)
    {
        if (item != null)
        {
            foreach (var slot in _invetorySlots)
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
        var heading = dropPosition - transform.position;
        var distance = heading.magnitude;
        var direction = heading / distance;
        Instantiate(item.ClickableGameObject, transform.position + direction, transform.rotation);
        return true;
    }

    //Called By UI Controller
    public Item SlotClicked(InventorySlot slot, Item item)
    {
        var savedItem = slot.Item;
        if (slot.Item != null)
        {
            slot.ClearSlot(slot.Item);
            if (item != null)
            {
                slot.AddItem(item);
            }
        }

        if (slot.Item == null && item != null)
        {
            slot.AddItem(item);
        }
        return savedItem;
    }
}
