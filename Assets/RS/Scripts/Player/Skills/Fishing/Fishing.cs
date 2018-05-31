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

    void OnEnable()
    {
        MouseController.OnClick += DoSkill;
    }

    void OnDisable()
    {
        MouseController.OnClick -= DoSkill;
    }


    void Start()
    {
        base.Start();
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
            var fishTime = _fishingTimer / FishingTime();
            base.ActivateSkill(fishTime);

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

    public void DoSkill(Clickable.ClickReturn clickReturn, Vector3 clickPosition)
    {
        if (clickReturn != null && _amFishing == false)
        {
            _amFishing = true;
            if (clickReturn.ClickAction == Clickable.ClickReturn.ClickActions.Fish)
            {
                Fish(clickReturn);
            }
        }
    }

    protected void Fish(Clickable.ClickReturn clickReturn)
    {
        StartSkill();
        _fishingPosition = transform.position;
        _fishingZone = clickReturn.ClickedObject.GetComponent<FishingZone>();
        _fishingRod.Cast(clickReturn.ClickPosition);
        _animationRouter.AnimationEventRouter(AnimationRouter.AnimationEvent.Cast);
    }

    private float FishingTime()
    {
        return 5.0f;
    }

    private void TryCatchFish()
    {
        var fish = _fishingZone.RequestCatch(_Level);
        CatchFish(fish);
    }

    private bool HasPlayerMovedWhileFishing()
    {
        if (Vector3.Distance(transform.position, _fishingPosition) >= 2.0f)
        {
            return true;
        }
        return false;
    }

    private void CatchFish(Fish fish)
    {
        if (fish != null)
        {
            AddXp(fish.RewardXp);
            _inventory.AddItem(fish);
            Reset();
        }
        else
        {
            Reset();
        }
    }

    private void Reset()
    {
        if (_amFishing)
        {
            _catchingFish = false;
            _amFishing = false;
            _fishingTimer = FishingTime();
            _fishingRod.ReelIn();
            EndSkill();
            _animationRouter.AnimationEventRouter(AnimationRouter.AnimationEvent.FishingEnd);
        }
    }
}
