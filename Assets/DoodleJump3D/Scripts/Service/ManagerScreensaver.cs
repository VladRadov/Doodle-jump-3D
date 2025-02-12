using UnityEngine;
using UnityEngine.UI;

public class ManagerScreensaver : BaseManager
{
    [SerializeField] private Image _screensaver;

    private AnimationPanel _animationPanel;

    public override void Initialize()
    {
        _animationPanel = new AnimationPanel(0.3f);
        _animationPanel.MoveDOAchorPosActiveObject(_screensaver.gameObject, new Vector2(0, -42));
    }
}
