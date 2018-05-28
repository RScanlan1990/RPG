using UnityEngine;

public class Skills : MonoBehaviour
{
    private Fishing _fishing;

    private void Start()
    {
        _fishing = gameObject.AddComponent<Fishing>();
    }

    public void DoSkill(Clickable.ClickReturn clickReturn)
    {
        switch(clickReturn.ClickAction)
        {
            case Clickable.ClickReturn.ClickActions.Fish:
                _fishing.enabled = true; 
                _fishing.Fish(clickReturn);
                break;
        }
    }
}