using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Fishing : Skill
{
    public Hook Hook;
    private Hook _hook;
    private Vector3 _fishingPosition;
    private GameObject _fishingRod;
    private FishingZone _fishingZone;
    private Equiped _equiped;
    private Inventory _inventory;

    new void Start()
    {
        base.Start();
        _equiped = gameObject.GetComponent<Equiped>();
        _inventory = gameObject.GetComponent<Inventory>();
    }

    new void Update()
    {
        if(_skilling)
        {
            base.Update();
            if (_inventory.NumberOfFreeSlots() <= 0)
            {
                Reset();
            }
        }
    }

    public void TryFish(Clickable.ClickReturn clickReturn)
    {
        _fishingZone = clickReturn.ClickedObject.GetComponent<FishingZone>();
        if (Vector3.Distance(transform.position, clickReturn.ClickPosition) <= _fishingZone.MinimuimDistance)
        {
            if (_inventory.NumberOfFreeSlots() > 0)
            {
                if(_equiped.HaveItemEquiped(_fishingZone.AcceptedTools))
                {
                    _fishingRod = _equiped.HaveToolTypeEquiped(Item.ItemType.Tool);
                    _skilling = true;
                    StartFishing(clickReturn);
                }
            }
        }
    }

    private void StartFishing(Clickable.ClickReturn clickReturn)
    {
        SendSkillAttemptedUiEvent();
        _fishingPosition = transform.position;
        StartCast(clickReturn.ClickPosition);
    }

    private void StartCast(Vector3 castPosition)
    {
        _hook = Instantiate(Hook, castPosition, Quaternion.identity);
        _hook.transform.SetParent(null);
        _hook.gameObject.SetActive(false);
        SendAnimationRouterEvent(AnimationRouter.AnimationEvent.Cast);
    }

    // Called By Animation Event(Cast Animation)
    public void SetFishingHookActive()
    {
        if (_hook.gameObject != null && _hook.gameObject.activeSelf == false)
        {
            _hook.gameObject.SetActive(true);
            _fishingRod.GetComponent<FishingRod_Loot>().SetFishingLinePostion(_hook.transform.position);
        }
    }

    // Called By Animation Event(Fishing Animation)
    public void TryCatchFish()
    {
        var fish = _fishingZone.RequestCatch(_level);
        CatchFish(fish);
    }

    private void CatchFish(Fish fish)
    {
        if(fish != null)
        {
            AddXp(fish.RewardXp);
            _inventory.AddItem(fish);
        }
        _skillAttempts++;
        SendSkillAttemptedUiEvent();
    }

    protected override void Reset()
    {
        if (_skilling)
        {
            base.Reset();
            ReelIn();
            SkillEnded();
            SendAnimationRouterEvent(AnimationRouter.AnimationEvent.FishingEnd);
        }
    }

    private void ReelIn()
    {
        Destroy(_hook.gameObject);
        _fishingRod.GetComponent<FishingRod_Loot>().ResetFishingLinePosition();
    }
}