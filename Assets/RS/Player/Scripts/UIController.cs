using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Slider SkillSlider;
    public Transform ContainerTransform;
    private List<Transform> _uiContainers = new List<Transform>();
    private Inventory _inventory;
    private Equiped _equiped;
    private MouseController _mouseController;
   
    private GameObject _activeUIPanel;

    [HideInInspector]
    public Item UiSelectedItem;

    void OnEnable()
    {
        MouseController.OnClick += MouseClicked;
        Skill.SkillActive += SkillActive;
        Skill.SkillEnded += SkillEnded;
    }

    void OnDisable()
    {
        MouseController.OnClick -= MouseClicked;
        Skill.SkillActive -= SkillActive;
        Skill.SkillEnded -= SkillEnded;
    }

    void Start()
    {
        _inventory = gameObject.GetComponent<Inventory>();
        _equiped = gameObject.GetComponent<Equiped>();
        _mouseController = gameObject.GetComponent<MouseController>();

        for (int i = 0; i < ContainerTransform.childCount; i++)
        {
            _uiContainers.Add(ContainerTransform.GetChild(i));
        }

        foreach (var container in _uiContainers)
        {
            container.gameObject.SetActive(false);
        }
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
	public void InventorySlotClicked(InventorySlot slot)
    {
        var item = _inventory.SlotClicked(slot, UiSelectedItem);
        UiSelectedItem = item;
        var selectedItemImage = UiSelectedItem != null ? UiSelectedItem.Image : null;
        _mouseController.AdjustMouseIcon(selectedItemImage);
    }

    //Called By UI Button
    public void EquipableSlotClicked(ItemSlot slot)
    {
        var item = _equiped.SlotClicked(slot, UiSelectedItem);
        UiSelectedItem = item;
        var selectedItemImage = UiSelectedItem != null ? UiSelectedItem.Image : null;
        _mouseController.AdjustMouseIcon(selectedItemImage);
    }


    #endregion

    #region Delegates
    private void MouseClicked(Clickable.ClickReturn clickReturn, Vector3 clickPosition, bool haveUiSelectedItem)
    {
        if (UiSelectedItem != null)
        {
            DropItem();
        }
    }
    #endregion

    #region Drop
    public void DropItem()
    {
        _inventory.DropItem(UiSelectedItem, _mouseController.GetWorldSpacePosition());
        UiSelectedItem = null;
        _mouseController.AdjustMouseIcon(null);
    }
    #endregion

    private void SkillActive(float skillTime, string skillName)
    {
        if (SkillSlider.transform.parent.gameObject.activeSelf == false)
        {
            SkillSlider.transform.parent.gameObject.SetActive(true);
        }
        SkillSlider.value = skillTime;
    }

    private void SkillEnded()
    {
        SkillSlider.transform.parent.gameObject.SetActive(false);
    }
}
