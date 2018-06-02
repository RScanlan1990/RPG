using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Items")]
public class Item : ScriptableObject
{
    public string Name;
    public Sprite Image;
    public GameObject ClickableGameObject;
}
