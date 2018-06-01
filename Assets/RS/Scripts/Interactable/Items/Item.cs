using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Items")]
public class Item : ScriptableObject
{
    public bool Equipable;
    public string Name;
    public Sprite Image;
    public GameObject ClickableGameObject;
}
