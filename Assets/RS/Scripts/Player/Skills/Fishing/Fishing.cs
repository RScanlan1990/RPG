using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Fishing : Skill
{
    private bool _catchingFish;
    private bool _amFishing;
    private float _fishingTimer;
    private Vector3 _fishingPosition;
    private AnimationRouter _animationRouter;
    private FishingRod _fishingRod;
    private FishingZone _fishingZone;
    private Inventory _inventory;

    void Start()
    {
        _catchingFish = false;
        _amFishing = false;
        _fishingTimer = FishingTime();
        _animationRouter = gameObject.GetComponent<AnimationRouter>();
        _fishingRod = gameObject.GetComponentInChildren<FishingRod>();
        _inventory = gameObject.GetComponent<Inventory>();
    }

    private void Update()
    {
        if (_amFishing)
        {
            if (_animationRouter.CurrentEvent == AnimationRouter.AnimationEvent.MidCast)
            {
                _fishingTimer -= Time.deltaTime;
            }

            if (_fishingTimer <= 0 && _catchingFish == false)
            {
                TryCatchFish();
            }

            if (HasPlayerMovedWhileFishing())
            {
                Reset();
            }
        }
    }

    private float FishingTime()
    {
        return 5.0f;
    }

    public void Fish(Clickable.ClickReturn clickReturn)
    {
        if (_amFishing == false)
        {
            _amFishing = true;
            _fishingPosition = transform.position;
            _fishingZone = clickReturn.ClickedObject.GetComponent<FishingZone>();
            _fishingRod.Cast(clickReturn.ClickPosition);
            _animationRouter.AnimationEventRouter(AnimationRouter.AnimationEvent.Cast);
        }
    }

    private void TryCatchFish()
    {
        var fish = _fishingZone.RequestCatch(_level);
        CatchFish(fish);
    }

    private void CatchFish(Fish fish)
    {
        if (fish != null)
        {
            _xp += fish.RewardXp;
            _inventory.AddItem(fish);
            CheckForLevelUp();
            Reset();
        }
        else
        {
            Reset();
        }
    }

    private void CheckForLevelUp()
    {
        var level = Levels.Where(l => l.Value <= _xp).ToList().OrderByDescending(l => l.Value).First();
        var difference = level.Key - _level;
        if (difference > 0)
        {
            for (int i = 0; i < difference; i++)
            {
                _level++;
                Debug.Log("Level Up!!!");
            }
        }
    }

    private bool HasPlayerMovedWhileFishing()
    {
        if (Vector3.Distance(transform.position, _fishingPosition) >= 2.0f)
        {
            return true;
        }
        return false;
    }

    private void Reset()
    {
        if (_amFishing)
        {
            _catchingFish = false;
            _amFishing = false;
            _fishingTimer = FishingTime();
            _fishingRod.ReelIn();
            _animationRouter.AnimationEventRouter(AnimationRouter.AnimationEvent.FishingEnd);
            this.enabled = false;
        }
    }
}
