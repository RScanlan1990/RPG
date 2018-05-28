﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Image MouseImage;
    private Inventory _inventory;
    private MousePointer _mousePointer;
    private Item _selectedItem;

    private GameObject _activeUIPanel;
    
    void Start()
    {
        _inventory = gameObject.GetComponent<Inventory>();
        _mousePointer = gameObject.GetComponent<MousePointer>();
    }

    void Update()
    {
        MouseImage.transform.position = _mousePointer.GetScreenPosition();
        if (_selectedItem != null)
        {
            SetIconToMousePostion();
        }

        if (_selectedItem == null)
        {
            DisableMouseImage();
        }
    }

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

    private void SetIconToMousePostion()
    {
        if (MouseImage.enabled == false)
        {
            MouseImage.sprite = _selectedItem.Image;
            MouseImage.enabled = true;
        }
    }

    public void DropItem()
    {
        
        _inventory.DropItem(_selectedItem, _mousePointer.GetWorldSpacePosition());
        _selectedItem = null;
    }

    private void DisableMouseImage()
    {
        if (MouseImage.enabled == true)
        {
            MouseImage.enabled = false;
        }
    }

    //Called By UI Button
    public void SlotClicked(InventorySlot slot)
    {
        var item = _inventory.SlotClicked(slot, _selectedItem);
        _selectedItem = item;
    }

    public bool HaveItemSelected()
    {
        return _selectedItem != null;
    }
}
