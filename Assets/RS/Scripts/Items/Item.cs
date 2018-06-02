using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Items")]
public class Item : ScriptableObject
{
    public enum ItemTypes
    {
        None,
        FishingRod,
    }

    public ItemTypes Type;
	public bool IsEquipable;
    public string Name;
    public Sprite Image;
    public GameObject ClickableGameObject;

}
