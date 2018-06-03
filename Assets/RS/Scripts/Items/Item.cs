using System.Collections;
using UnityEngine;

public class Item : MonoBehaviour
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
}
