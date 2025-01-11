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
        _animationPanel = new AnimationPanel(new Vector2(502, -232), new Vector2(502, 500), 0.3f);

        _clue1.OnOkClick.Subscribe(_ => { _animationPanel.MoveDOAchorPos(_clue2.gameObject, _clue1.gameObject); });
        _clue2.OnOkClick.Subscribe(_ => { _animationPanel.MoveDOAchorPos(_clue3.gameObject, _clue2.gameObject); });
        _clue3.OnOkClick.Subscribe(_ => { _animationPanel.MoveDOAchorPos(_clue4.gameObject, _clue3.gameObject); });
        _clue4.OnOkClick.Subscribe(_ =>
        {
            _animationPanel.MoveDOAchorPos(_clue4.gameObject, _clue4.gameObject);
            OnEducationEnd.Execute();
        });

        ManagerUniRx.AddObjectDisposable(OnEducationEnd);
    }

    public void SetActive(bool value)
    {
        _parentPanel.gameObject.SetActive(value);

        if(value)
            _animationPanel.MoveDOAchorPos(_clue1.gameObject);
    }

    private void OnDestroy()
    {
        ManagerUniRx.Dispose(OnEducationEnd);
    }
}
