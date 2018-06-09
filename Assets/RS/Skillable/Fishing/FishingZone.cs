using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FishingZone : Skillable
{
    public Fish RequestCatch(int playerLevel)
    {
        List<Item> items = GetAllFishWithinLevel(playerLevel).ConvertAll(f => (Item)f);
        return (Fish)ChooseItem(items);
    }

    private List<Fish> GetAllFishWithinLevel(int playerLevel)
    {
        var rewardFish = RewardItems.ConvertAll(i => (Fish)i);
        var fishWithinLevel = new List<Fish>();
        foreach (var fish in rewardFish)
        {
            if(fish != null)
            {
                if (playerLevel >= fish.RequiredLevel)
                {
                    rewardFish.Add(fish);
                }
            }
        }
        return rewardFish;
    }
}
