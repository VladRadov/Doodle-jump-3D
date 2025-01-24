using UnityEngine;
using UnityEngine.UI;

public class AchievementItemView : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Text _name;
    [SerializeField] private Text _description;
    [SerializeField] private Image _icon;

    public void Initialize(Achievement achievement)
    {
        _name.text = achievement.Name;
        _description.text = achievement.Description;
        _icon.sprite = achievement.Icon;
    }
}
