using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Skill : MonoBehaviour
{
    public SkillSlot SkillSlot;
    protected float _Xp = 0;
    protected int _Level = 1;
    protected Dictionary<int, int> _Levels = new Dictionary<int, int>
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

    protected void Start()
    {
        _uiController = gameObject.GetComponent<UIController>();
        UpdateUiSkillSlot(_Level.ToString(), _Xp.ToString());
    }

    protected void UpdateUiSkillSlot(string level, string xp)
    {
        _uiController.UpdateSkillSlot(SkillSlot, level, xp);
    }

    protected void SkillActive(float skillTime)
    {
        _uiController.SkillActive(skillTime);
    }

    protected void SkillEnded()
    {
        _uiController.SkillEnded();
    }

    protected void AddXp(float xp)
    {
        _Xp += xp;
        CheckForLevelUp();
        UpdateUiSkillSlot(_Level.ToString(), _Xp.ToString());
    }

    private void CheckForLevelUp()
    {
        var level = _Levels.Where(l => l.Value <= _Xp).ToList().OrderByDescending(l => l.Value).First();
        var difference = level.Key - _Level;
        if (difference > 0)
        {
            for (int i = 0; i < difference; i++)
            {
                _Level++;
                SkillSlot.UpdateSlotLevel(_Level.ToString());
            }
        }
    }
}
