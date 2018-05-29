using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Image MouseImage;
    private Inventory _inventory;
    private MouseController _mouseController;
    private Item _selectedItem;

    private GameObject _activeUIPanel;

    void OnEnable()
    {
        MouseController.OnClick += MouseClicked;
    }

    void OnDisable()
    {
        MouseController.OnClick -= MouseClicked;
    }


    void Start()
    {
        _inventory = gameObject.GetComponent<Inventory>();
        _mouseController = gameObject.GetComponent<MouseController>();
    }

    void Update()
    {
        MouseImage.transform.position = _mouseController.GetScreenPosition();
    }

    #region SwapPanel
    //Called By UI Button
    public void SwapUiPanel(GameObject panel)
    {
        if (_activeUIPanel != null)
        {
            _activeUIPanel.SetActive(false);
        }
        _activeUIPanel = panel;
        panel.SetActive(true);
    }
    #endregion

    #region Slot Clicked
    //Called By UI Button
    public void SlotClicked(InventorySlot slot)
    {
        var item = _inventory.SlotClicked(slot, _selectedItem);
        _selectedItem = item;
        AdjustMouseIcon();
    }

    private void AdjustMouseIcon()
    {
        if (_selectedItem != null)
        {
            MouseImage.sprite = _selectedItem.Image;
            MouseImage.enabled = true;
        }
        else
        {
            DisableMouseImage();
        }
    }
    #endregion

    #region Delegates
    private void MouseClicked(Clickable.ClickReturn clickReturn, Vector3 clickPosition)
    {
        if (_selectedItem != null)
        {
            DropItem();
        }
    }
    #endregion

    #region Drop
    public void DropItem()
    {
        _inventory.DropItem(_selectedItem, _mouseController.GetWorldSpacePosition());
        _selectedItem = null;
        DisableMouseImage();
    }

    private void DisableMouseImage()
    {
        if (MouseImage.enabled == true)
        {
            MouseImage.enabled = false;
        }
    }
    #endregion
}
