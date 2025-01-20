using UnityEngine;
using UnityEngine.UI;

public class AchivementView : MonoBehaviour
{
    private AnimationPanel _animationPanel;

    [SerializeField] private Text _descriptionAchievement;
    [SerializeField] private Slider _progressAchievement;
    [SerializeField] private Vector2 endMovePositionAnimation;

    public void Initialize(Achivement achivement)
    {
        _animationPanel = new AnimationPanel(0.3f);

        SetDescriptionAchievement(achivement.Description);
        SetMaxProgressAchievement(achivement.MaxCountSuccess);
        SetProgressAchievement(achivement.CurrentCountSuccess);

        _animationPanel.MoveDOAchorPosActiveObject(gameObject, endMovePositionAnimation);
    }

    public void SetProgressAchievement(int progressAchievement)
        => _progressAchievement.value = progressAchievement;

    private void SetDescriptionAchievement(string descriptionAchievement)
        => _descriptionAchievement.text = descriptionAchievement;

    private void SetMaxProgressAchievement(int maxProgressAchievement)
        => _progressAchievement.maxValue = maxProgressAchievement;

    private void OnValidate()
    {
        if (_progressAchievement == null)
            _progressAchievement = GetComponent<Slider>();
    }
}
