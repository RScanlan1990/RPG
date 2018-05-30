using System.Collections.Generic;
using UnityEngine;

public class Skills : MonoBehaviour
{
    private Fishing _fishing;
    public List<Skill> SkillList;

    void OnEnable()
    {
        MouseController.OnClick += DoSkill;
    }

    void OnDisable()
    {
        MouseController.OnClick -= DoSkill;
    }

    private void Start()
    {
        AddSkills();
        _fishing = gameObject.AddComponent<Fishing>();
    }

    private void AddSkills()
    {
        foreach (var skill in SkillList)
        {

        }
    }

    public void DoSkill(Clickable.ClickReturn clickReturn, Vector3 clickPosition)
    {
        if (clickReturn != null)
        {
            if (clickReturn.ClickType == Clickable.ClickReturn.ClickTypes.Skill)
            {
                switch (clickReturn.ClickAction)
                {
                    case Clickable.ClickReturn.ClickActions.Fish:
                        _fishing.enabled = true;
                        _fishing.Fish(clickReturn);
                        break;
                }
            }
        }
    }
}