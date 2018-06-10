using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Fishing))]
public class Skill_Router : MonoBehaviour
{
    private Fishing _fising;

    private void Start()
    {
        _fising = gameObject.GetComponent<Fishing>();

    }

    void OnEnable()
    {
        MouseController.OnClick += DoSkill;
    }

    void OnDisable()
    {
        MouseController.OnClick -= DoSkill;
    }

    private void DoSkill(Clickable.ClickReturn clickReturn, Vector3 clickPostion, bool haveUiSelectedItem)
    {
        if (clickReturn != null)
        {
            if (clickReturn.ClickAction == Clickable.ClickReturn.ClickActions.Fish)
            {
                _fising.enabled = true;
                _fising.TryFish(clickReturn);
            }
        }
    }
}
