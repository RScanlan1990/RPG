using UnityEngine;

public class Skills : MonoBehaviour
{
    private Fishing _fishing;

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
        _fishing = gameObject.AddComponent<Fishing>();
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