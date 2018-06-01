using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Fishing : Skill
{
    public Hook Hook;
    private Hook _hook;

    private bool _amFishing;
    private Vector3 _fishingPosition;
    private AnimationRouter _animationRouter;
    //private FishingRod _fishingRod;
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
        _amFishing = false;
        _animationRouter = gameObject.GetComponent<AnimationRouter>();
       // _fishingRod = gameObject.GetComponentInChildren<FishingRod>();
        _inventory = gameObject.GetComponent<Inventory>();
    }

    private void Update()
    {
        HasPlayerMovedWhileFishing();
    }

    public void DoSkill(Clickable.ClickReturn clickReturn, Vector3 clickPosition)
    {
        if (clickReturn != null && _amFishing == false)
        {
            _amFishing = true;
            if (clickReturn.ClickAction == Clickable.ClickReturn.ClickActions.Fish)
            {
                StartFishing(clickReturn);
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

    
    // Called By Animation Event
    public void SetFishingHookActive()
    {
        if (_hook.gameObject != null && _hook.gameObject.activeSelf == false)
        {
            _hook.gameObject.SetActive(true);
           // _fishingRod.SetFishingLinePostion(_hook.transform.position);
        }
    }

    // Called By Animation Event
    public void TryCatchFish()
    {
        var fish = _fishingZone.RequestCatch(_Level);
        CatchFish(fish);
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
            _amFishing = false;
            ReelIn();
            EndSkill();
           // _fishingRod.ResetFishingLinePosition();
            _animationRouter.AnimationEventRouter(AnimationRouter.AnimationEvent.FishingEnd);
        }
    }

    public void ReelIn()
    {
        Destroy(_hook);
    }

    private float FishingTime()
    {
        return 5.0f;
    }

    private void HasPlayerMovedWhileFishing()
    {
        if (_amFishing && Vector3.Distance(transform.position, _fishingPosition) >= 2.0f)
        {
            Reset();
        }
    }
}