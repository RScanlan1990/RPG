using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Slider SkillSlider;
    public Image MouseImage;
    private Inventory _inventory;
    private Equiped _equiped;
    private MouseController _mouseController;
    private Item _selectedItem;
    private GameObject _activeUIPanel;

    void OnEnable()
    {
        MouseController.OnClick += MouseClicked;
        Skill.SkillActive += SkillActive;
        Skill.SkillEnded += SkillEnded;
        Skill.SkillStarted += SkillStarted;
    }

    void OnDisable()
    {
        MouseController.OnClick -= MouseClicked;
        Skill.SkillActive -= SkillActive;
        Skill.SkillEnded -= SkillEnded;
        Skill.SkillStarted -= SkillStarted;
    }

    void Start()
    {
        _inventory = gameObject.GetComponent<Inventory>();
        _equiped = gameObject.GetComponent<Equiped>();
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
	public void InventorySlotClicked(ItemSlot slot)
    {
        var item = _inventory.SlotClicked(slot, _selectedItem);
        _selectedItem = item;
        AdjustMouseIcon();
    }

    //Called By UI Button
    public void EquipableSlotClicked(ItemSlot slot)
    {
        var item = _equiped.SlotClicked(slot, _selectedItem);
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

    private void SkillActive(float skillTime)
    {
        if (SkillSlider.transform.parent.gameObject.activeSelf == false)
        {
            SkillSlider.transform.parent.gameObject.SetActive(true);
        }
        SkillSlider.value = skillTime;
    }

    private void SkillStarted()
    {
        SkillSlider.transform.parent.gameObject.SetActive(true);
    }

    private void SkillEnded()
    {
        SkillSlider.transform.parent.gameObject.SetActive(false);
    }
}
