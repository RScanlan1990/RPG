using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Skill : MonoBehaviour
{
    public SkillSlot SkillSlot;
    protected float _xp = 0;
    protected int _level = 1;
    protected int _skillAttempts = 0;
    protected bool _skilling = false;
    protected Dictionary<int, int> _levels = new Dictionary<int, int>
    {
        {1, 0},
        {2, 10},
        {3, 20},
        {4, 40},
        {5, 80},
        {6, 160},
        {7, 320},
        {8, 640},
        {9, 1280},
        {10, 2560},
        {11, 5120},
        {12, 10240},
        {13, 20480},
        {14, 40960},
        {15, 81920},
    };
    private UIController _uiController;
    private AnimationRouter _animationRouter;

    protected void OnEnable()
    {
        Movement.OnMove += Reset;
    }

    protected void OnDisable()
    {
        Movement.OnMove -= Reset;
    }
    protected void Start()
    {
        _uiController = gameObject.GetComponent<UIController>();
        _animationRouter = gameObject.GetComponent<AnimationRouter>();
        UpdateUiSkillSlot(_level.ToString(), _xp.ToString());
    }

    protected void Update()
    {
        if(_skillAttempts >= 5)
        {
            Reset();
        }
    }

    protected void SkillEnded()
    {
        _uiController.SkillEnded();
    }

    protected void AddXp(float xp)
    {
        _xp += xp;
        CheckForLevelUp();
        UpdateUiSkillSlot(_level.ToString(), _xp.ToString());
    }

    private void UpdateUiSkillSlot(string level, string xp)
    {
        _uiController.UpdateSkillSlot(SkillSlot, level, xp);
    }

    private void CheckForLevelUp()
    {
        var level = _levels.Where(l => l.Value <= _xp).ToList().OrderByDescending(l => l.Value).First();
        var difference = level.Key - _level;
        if (difference > 0)
        {
            for (int i = 0; i < difference; i++)
            {
                _level++;
                SkillSlot.UpdateSlotLevel(_level.ToString());
            }
        }
    }

    protected virtual void Reset()
    {
        _skilling = false;
        _skillAttempts = 0;
    }

    protected void SendAnimationRouterEvent(AnimationRouter.AnimationEvent animationEvent)
    {
        _animationRouter.AnimationEventRouter(animationEvent);
    }

    protected void SendSkillAttemptedUiEvent()
    {
        SkillAttempted();
    }

    private void SkillAttempted()
    {
        _uiController.SkillActive(_skillAttempts / 5.0f);
    }
}
