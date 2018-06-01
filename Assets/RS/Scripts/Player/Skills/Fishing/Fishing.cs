using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Fishing : Skill
{
    public GameObject Hook;
    private GameObject _hook;
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
        if (HasPlayerMovedWhileFishing())
        {
            Reset();
        }
    }

    public void Fish()
    {
        if (_amFishing == false)
        {
            TryCatchFish();
        } else
        if (_hook.activeSelf == false)
        {
            SetHookActive();
        }
    }

    public void DoSkill(Clickable.ClickReturn clickReturn, Vector3 clickPosition)
    {
        if (clickReturn != null && _amFishing == false)
        {
            if (clickReturn.ClickAction == Clickable.ClickReturn.ClickActions.Fish)
            {
                StartFishing(clickReturn);
            }
        }
    }

    private float FishingTime()
    {
        return 5.0f;
    }

    private void SetHookActive()
    {
        if (_hook.gameObject != null && _hook.activeSelf == false)
        {
            _hook.SetActive(true);
        }
    }

    private void StartFishing(Clickable.ClickReturn clickReturn)
    {
        SkillStartedEvent();
        _fishingPosition = transform.position;
        _fishingZone = clickReturn.ClickedObject.GetComponent<FishingZone>();
        Cast(clickReturn.ClickPosition);
    }

    private void Cast(Vector3 castPosition)
    {
        _hook = Instantiate(Hook, castPosition, Quaternion.identity);
        _hook.transform.SetParent(null);
        _hook.SetActive(false);
        _animationRouter.AnimationEventRouter(AnimationRouter.AnimationEvent.Cast);
        _amFishing = true;
        Debug.Log("Casted");
    }

    private void TryCatchFish()
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
            _catchingFish = false;
            _amFishing = false;
            _fishingTimer = FishingTime();
            ReelIn();
            EndSkill();
            _animationRouter.AnimationEventRouter(AnimationRouter.AnimationEvent.FishingEnd);
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
    
    public void ReelIn()
    {
        Destroy(_hook);
    }
}