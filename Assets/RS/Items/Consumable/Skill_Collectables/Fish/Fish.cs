using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewFish", menuName = "Items/Consumable/Fish", order = 1)]
public class Fish : Item {
    public int RequiredLevel;
    public int MaximumLevel;
    public float ChanceToCatch;
    public float RewardXp;
}
