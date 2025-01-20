using UnityEngine;
using UniRx;

public class ManagerEducation : BaseManager
{
    private AnimationPanel _animationPanel;

    [SerializeField] private GameObject _parentPanel;
    [SerializeField] private ClueView _clue1;
    [SerializeField] private ClueView _clue2;
    [SerializeField] private ClueView _clue3;
    [SerializeField] private ClueView _clue4;

    public ReactiveCommand OnEducationEnd = new();

    public override void Initialize()
    {
        _animationPanel = new AnimationPanel(0.3f);

        _clue1.OnOkClick.Subscribe(async _ =>
        {
            await _animationPanel.MoveDOAchorPosHideObject(_clue1.gameObject, new Vector2(502, 500));
            _animationPanel.MoveDOAchorPosActiveObject(_clue2.gameObject, new Vector2(502, -232));
        });
        _clue2.OnOkClick.Subscribe(async _ =>
        {
            await _animationPanel.MoveDOAchorPosHideObject(_clue2.gameObject, new Vector2(502, 500));
            _animationPanel.MoveDOAchorPosActiveObject(_clue3.gameObject, new Vector2(502, -232));
        });
        _clue3.OnOkClick.Subscribe(async _ =>
        {
            await _animationPanel.MoveDOAchorPosHideObject(_clue3.gameObject, new Vector2(502, 500));
            _animationPanel.MoveDOAchorPosActiveObject(_clue4.gameObject, new Vector2(502, -232));
        });
        _clue4.OnOkClick.Subscribe(async _ =>
        {
            await _animationPanel.MoveDOAchorPosHideObject(_clue4.gameObject, new Vector2(502, 500));
            OnEducationEnd.Execute();
        });

        ManagerUniRx.AddObjectDisposable(OnEducationEnd);
    }

    public void SetActive(bool value)
    {
        _parentPanel.gameObject.SetActive(value);

        if (value)
            _animationPanel.MoveDOAchorPosActiveObject(_clue1.gameObject, new Vector2(502, -232));
    }

    private void OnDestroy()
    {
        ManagerUniRx.Dispose(OnEducationEnd);
    }
}
