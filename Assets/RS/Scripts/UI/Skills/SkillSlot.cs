using UnityEngine;
using UnityEngine.UI;

public class SkillSlot : MonoBehaviour
{
    private Image _icon;
    private Text _levelText;
    private Text _levelXp;

    void Awake()
    {
        var icon = gameObject.transform.Find("Icon");
        var level = gameObject.transform.Find("Level");
        var xp = gameObject.transform.Find("Xp");
        _icon = icon.GetComponent<Image>();
        _levelText = level.GetComponent<Text>();
        _levelXp = xp.GetComponent<Text>();
    }

    public void UpdateSlotLevelAndXp(string level, string xp)
    {
        _levelText.text = level;
        _levelXp.text = xp;
    }

    public void UpdateSlotLevel(string level)
    {
        _levelText.text = level;
    }

    public void UpdateSlotXp(string xp)
    {
        _levelXp.text = xp;
    }
}
