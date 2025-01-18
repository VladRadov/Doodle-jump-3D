using UnityEngine;
using UnityEngine.UI;

public class AchivementView : MonoBehaviour
{
    [SerializeField] private Text _descriptionAchievement;
    [SerializeField] private Slider _progressAchievement;

    public void SetDescriptionAchievement(string descriptionAchievement)
        => _descriptionAchievement.text = descriptionAchievement;

    public void SetProgressAchievement(int progressAchievement)
        => _progressAchievement.value = progressAchievement;

    public void SetMaxProgressAchievement(int maxProgressAchievement)
        => _progressAchievement.maxValue = maxProgressAchievement;

    private void OnValidate()
    {
        if (_progressAchievement == null)
            _progressAchievement = GetComponent<Slider>();
    }
}
