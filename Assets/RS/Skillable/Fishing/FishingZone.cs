using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FishingZone : Skillable
{
    public Fish RequestCatch(int playerLevel)
    {
        List<Item> items = GetAllFishWithinLevel(playerLevel).ConvertAll(f => (Item)f);
        return (Fish)ChooseItemRandomItemInList(items);
    }

    private List<Fish> GetAllFishWithinLevel(int playerLevel)
    {
        var possibleFish = new List<Fish>(RewardItems.ConvertAll(i => (Fish)i));
        var fishes = new List<Fish>();
        foreach (var fish in possibleFish)
        {
            if(fish != null)
            {
                if (playerLevel >= fish.RequiredLevel)
                {
                    fishes.Add(fish);
                }
            }
        }
        return fishes;
    }
}
