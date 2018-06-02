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
        _fishingAttempts = 0;
        _animationRouter = gameObject.GetComponent<AnimationRouter>();
        _equiped = gameObject.GetComponent<Equiped>();
        _inventory = gameObject.GetComponent<Inventory>();
    }

    private void Update()
    {
        if(_amFishing)
        {
            HasPlayerMovedWhileFishing();
            if (_fishingAttempts >= 5)
            {
                Reset();
            }
        }
    }

    public void DoSkill(Clickable.ClickReturn clickReturn, Vector3 clickPosition)
    {
        if (clickReturn != null && _amFishing == false)
        {
            if (clickReturn.ClickAction == Clickable.ClickReturn.ClickActions.Fish)
            {
                _amFishing = true;
                if (_equiped.HaveToolTypeEquiped(Item.ItemTypes.FishingRod))
                {
                    StartFishing(clickReturn);
                }
            }
        }
    }

    private void StartFishing(Clickable.ClickReturn clickReturn)
    {
        SkillStartedEvent();
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
            //_fishingRod.SetFishingLinePostion(_hook.transform.position);
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
    }

    private void Reset()
    {
        if (_amFishing)
        {
            _amFishing = false;
            _fishingAttempts = 0;
            ReelIn();
            EndSkill();
            //_fishingRod.ResetFishingLinePosition();
            _animationRouter.AnimationEventRouter(AnimationRouter.AnimationEvent.FishingEnd);
        }
    }

    public void ReelIn()
    {
        DestroyImmediate(_hook.gameObject);
    }

    private void HasPlayerMovedWhileFishing()
    {
        if (_amFishing && Vector3.Distance(transform.position, _fishingPosition) >= 2.0f)
        {
            Reset();
        }
    }
}