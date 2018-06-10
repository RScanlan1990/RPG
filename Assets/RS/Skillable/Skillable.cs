using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skillable : Clickable {

    public List<Item> RewardItems;

    protected Item ChooseItemRandomItemInList(List<Item> items)
    {
        return items[Random.Range(0, items.Count)];
    }
}
