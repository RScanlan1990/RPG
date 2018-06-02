using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Items")]
public class Item : ScriptableObject
{
    public enum ItemType
    {
        None, Tool
    }

    public ItemType Type;
	public bool IsEquipable;
    public string Name;
    public Sprite Image;
    public GameObject ClickableGameObject;

    private bool _isValid = true;

    public Item()
    {
        _isValid = false;
    }

    public bool IsItemValid()
    {
        return _isValid;
    }
}
