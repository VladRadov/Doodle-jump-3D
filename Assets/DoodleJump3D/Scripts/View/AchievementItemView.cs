using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AchievementItemView : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private TextMeshProUGUI _description;
    [SerializeField] private Image _icon;

    public void Initialize(Achievement achievement)
    {
        _name.text = achievement.Name;
        _description.text = achievement.Description;
        _icon.sprite = achievement.Icon;
    }
}
