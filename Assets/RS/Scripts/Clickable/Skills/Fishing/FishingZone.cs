using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FishingZone : Clickable
{
    private int _playerLevel;

    public List<Fish> Fishes = new List<Fish>();

    public Fish RequestCatch(int playerLevel)
    {
        _playerLevel = playerLevel;
        Debug.Log("RequestCatch");
        return ChooseFish(GetAllFishWithinLevel(playerLevel));
    }

    private Fish ChooseFish(List<Fish> fishes)
    {
        Debug.Log("ChooseFish");
        var probabilties = GetProbabilties(fishes);
        var total = GetProbabilityTotal(probabilties);
        var randomPoint = Random.value * total;
        for (var i = 0; i < probabilties.Count(); i++)
        {
            if (randomPoint < probabilties[i] && i <= fishes.Count - 1)
            {
                return fishes[i];
            }
            randomPoint -= probabilties[i];
        }
        return new Fish();
    }

    private List<float> GetProbabilties(List<Fish> fishes)
    {
        var probabilties = new List<float>();
        foreach (var fish in fishes)
        {
            probabilties.Add(fish.ChanceToCatch);
        }
        probabilties.Add(ChanceNotToCatch(_playerLevel));
        return probabilties;
    }

    private float GetProbabilityTotal(List<float> probabilties)
    {
        var total = 0.0f;
        foreach (float elem in probabilties)
        {
            total += elem;
        }
        return total;
    }
    
    private List<Fish> GetAllFishWithinLevel(int playerLevel)
    {
        return Fishes
            .Where(fish => playerLevel >= fish.RequiredLevel && playerLevel <= fish.MaximumLevel)
            .ToList();
    }

    private float ChanceNotToCatch(int playerLevel)
    {
        var chanceToCatch = 50.0f - playerLevel;
        return chanceToCatch / 50.0f;
    }
}
