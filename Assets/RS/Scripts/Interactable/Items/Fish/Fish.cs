using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Fish", menuName = "Items/Fishing/Fish")]
public class Fish : Item {
    public int RequiredLevel;
    public int MaximumLevel;
    public float ChanceToCatch;
    public float RewardXp;
}
