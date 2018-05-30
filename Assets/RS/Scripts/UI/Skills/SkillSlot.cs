using UnityEngine;
using UnityEngine.UI;

public class SkillSlot : MonoBehaviour
{

    public Text LevelText;
    public Image SkillIcon;

    public void UpdateLevel(string level)
    {
        LevelText.text = level;
    }
}
