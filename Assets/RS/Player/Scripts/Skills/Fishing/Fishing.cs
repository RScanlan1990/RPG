using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Fishing : Skill
{
    public Hook Hook;
    private Hook _hook;
    private bool _amFishing;
    private int _fishingAttempts;
    private Vector3 _fishingPosition;
    private GameObject _fishingRod;
    private AnimationRouter _animationRouter;
    private FishingZone _fishingZone;
    private Equiped _equiped;
    private Inventory _inventory;

    void Start()
    {
        base.Start();
        _amFishing = false;
        _fishingAttempts = 1;
        _animationRouter = gameObject.GetComponent<AnimationRouter>();
        _equiped = gameObject.GetComponent<Equiped>();
        _inventory = gameObject.GetComponent<Inventory>();
        this.enabled = false;
    }

    private void Update()
    {
        if(_amFishing)
        {
            if (_equiped.HaveToolTypeEquiped(Item.ItemTypes.Tool) == null)
            {
                Reset(true);
            }
            else
            if(_fishingAttempts >= 6 || HasPlayerMovedWhileFishing() == true)
            {
                Reset(false);
            }
            if (_inventory.NumberOfFreeSlots() <= 0)
            {
                Reset(false);
            }
        }
    }

    public void TryFish(Clickable.ClickReturn clickReturn)
    {
        if( _amFishing == false)
        {
            _fishingZone = clickReturn.ClickedObject.GetComponent<FishingZone>();
            if (Vector3.Distance(transform.position, clickReturn.ClickPosition) <= _fishingZone.MinimuimDistance)
            {
                if (_inventory.NumberOfFreeSlots() > 0)
                {
                    //Needs to be specific fishing rod
                    _fishingRod = _equiped.HaveToolTypeEquiped(Item.ItemTypes.Tool);
                    if (_fishingRod != null)
                    {
                        _amFishing = true;
                        StartFishing(clickReturn);
                    }
                }
            }
        }
    }

    private void StartFishing(Clickable.ClickReturn clickReturn)
    {
        SkillActive(_fishingAttempts / 5.0f);
        _fishingPosition = transform.position;
        StartCast(clickReturn.ClickPosition);
    }

    private void StartCast(Vector3 castPosition)
    {
        _hook = Instantiate(Hook, castPosition, Quaternion.identity);
        _hook.transform.SetParent(null);
        _hook.gameObject.SetActive(false);
        _animationRouter.AnimationEventRouter(AnimationRouter.AnimationEvent.Cast);
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
        var fish = _fishingZone.RequestCatch(_Level);
        CatchFish(fish);
    }

    private void CatchFish(Fish fish)
    {
        if(fish != null)
        {
            AddXp(fish.RewardXp);
            _inventory.AddItem(fish);
        }
        _fishingAttempts++;
        SkillActive(_fishingAttempts / 5.0f, "Fishing");
    }

    private void Reset(bool fishingRodDestroyed)
    {
        if (_amFishing)
        {
            _amFishing = false;
            _fishingAttempts = 1;
            ReelIn();
            SkillEnded();
            _animationRouter.AnimationEventRouter(AnimationRouter.AnimationEvent.FishingEnd);
            if(fishingRodDestroyed == false)
            {
                _fishingRod.GetComponent<FishingRod_Loot>().ResetFishingLinePosition();
            }
            this.enabled = false;
        }
    }

    public void ReelIn()
    {
        Destroy(_hook.gameObject);
    }

    private bool HasPlayerMovedWhileFishing()
    {
        return Vector3.Distance(transform.position, _fishingPosition) >= 2.0f;
    }
}