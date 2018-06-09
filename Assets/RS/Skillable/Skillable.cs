using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skillable : Clickable {

    public List<Item> RewardItems;
    public List<float> RewardItemsProbabilties = new List<float>();

    protected Item ChooseItem(List<Item> items)
    {
        var probabilties = RewardItemsProbabilties;
        var total = GetProbabilityTotal();
        var randomPoint = Random.value * total;
        for (var i = 0; i < RewardItemsProbabilties.Count; i++)
        {
            if (randomPoint < probabilties[i])
            {
                return items[i];
            }
            randomPoint -= probabilties[i];
        }
        return null;
    }

    protected float GetProbabilityTotal()
    {
        var total = 0.0f;
        foreach (float elem in RewardItemsProbabilties)
        {
            total += elem;
        }
        return total;
    }
}
