using UnityEngine;
using UnityEngine.UI;

using TMPro;

public class AchievementItemView : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private TextMeshProUGUI _description;
    [SerializeField] private Image _icon;
    [Header("Data")]
    [SerializeField] private Achievement _achievement;

    public void Initialize(Achievement achievement)
    {
        _achievement = achievement;

        SetAchivementDescription();
        SetActiveAchivementItem();
    }

    private void OnEnable()
    {
        SetAchivementDescription();
    }

    private void SetAchivementDescription()
    {
        if (_achievement != null)
        {
            _name.text = _achievement.Name;
            _description.text = _achievement.Description;
            _icon.sprite = _achievement.Icon;
        }
    }

    private void SetActiveAchivementItem()
        => _icon.color = _achievement.IsAchievementSuccess ? new Color(1, 1, 1, 1) : new Color(1, 1, 1, 0.5f);
}
