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
        _amFishing = false;
        _fishingAttempts = 1;
        _animationRouter = gameObject.GetComponent<AnimationRouter>();
        _equiped = gameObject.GetComponent<Equiped>();
        _inventory = gameObject.GetComponent<Inventory>();
    }

    private void Update()
    {
        if(_amFishing)
        {
            if (_equiped.HaveToolTypeEquiped(Item.ItemTypes.FishingRod) == null)
            {
                Reset(true);
            }
            else
            if(_fishingAttempts >= 6 || HasPlayerMovedWhileFishing() == true)
            {
                Reset(false);
            }    
        }
    }

    public void DoSkill(Clickable.ClickReturn clickReturn, Vector3 clickPosition)
    {
        if(clickReturn != null && _amFishing == false)
        {
            if (clickReturn.ClickAction == Clickable.ClickReturn.ClickActions.Fish)
            {
                if (_inventory.NumberOfFreeSlots() > 0)
                {
                    _fishingRod = _equiped.HaveToolTypeEquiped(Item.ItemTypes.FishingRod);
                    _amFishing = true;
                    StartFishing(clickReturn);
                }
            }
        }
    }

    private void StartFishing(Clickable.ClickReturn clickReturn)
    {
        SkillActiveEvent(_fishingAttempts / 5.0f, "Fishing");
        _fishingPosition = transform.position;
        _fishingZone = clickReturn.ClickedObject.GetComponent<FishingZone>();
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
        if(_inventory.NumberOfFreeSlots() > 0)
        {
            var fish = _fishingZone.RequestCatch(_Level);
            CatchFish(fish);
        } else
        {
            Reset(false);
        }
    }

    private void CatchFish(Fish fish)
    {
        if(fish != null)
        {
            AddXp(fish.RewardXp);
            _inventory.AddItem(fish);
        }
        _fishingAttempts++;
        SkillActiveEvent(_fishingAttempts / 5.0f, "Fishing");
    }

    private void Reset(bool fishingRodDestroyed)
    {
        if (_amFishing)
        {
            _amFishing = false;
            _fishingAttempts = 1;
            ReelIn();
            EndSkill();
            _animationRouter.AnimationEventRouter(AnimationRouter.AnimationEvent.FishingEnd);
            if(fishingRodDestroyed == false)
            {
                _fishingRod.GetComponent<FishingRod_Loot>().ResetFishingLinePosition();
            }
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