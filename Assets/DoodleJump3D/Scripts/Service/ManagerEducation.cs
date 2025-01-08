using UnityEngine;
using UniRx;
using DG.Tweening;
using Cysharp.Threading.Tasks;

public class ManagerEducation : BaseManager
{
    private Vector2 START_Y_POSITION = new Vector2(502, -232);
    private Vector2 END_Y_POSITION = new Vector2(502, 500);
    private float SPEED_MOVE_Y_POSITION = 0.3f;

    [SerializeField] private GameObject _parentPanel;
    [SerializeField] private ClueView _clue1;
    [SerializeField] private ClueView _clue2;
    [SerializeField] private ClueView _clue3;
    [SerializeField] private ClueView _clue4;

    public ReactiveCommand OnEducationEnd = new();

    public override void Initialize()
    {
        _clue1.OnOkClick.Subscribe(_ => { MoveCluePanel(_clue1, _clue2); });
        _clue2.OnOkClick.Subscribe(_ => { MoveCluePanel(_clue2, _clue3); });
        _clue3.OnOkClick.Subscribe(_ => { MoveCluePanel(_clue3, _clue4); });
        _clue4.OnOkClick.Subscribe(async _ =>
        {
            MoveCluePanel(null, _clue4);
            await UniTask.Delay(500);

            _clue4.SetActive(false);
            OnEducationEnd.Execute();
        });

        ManagerUniRx.AddObjectDisposable(OnEducationEnd);
    }

    public void SetActive(bool value)
    {
        _parentPanel.gameObject.SetActive(value);
        MoveCluePanel(null, _clue1);
    }

    private async void MoveCluePanel(ClueView clueNoActive, ClueView clueActive)
    {
        clueActive?.SetActive(true);

        if (clueNoActive != null)
        {
            var recTransformClueNoActive = clueNoActive.GetComponent<RectTransform>();
            recTransformClueNoActive.DOAnchorPos(END_Y_POSITION, SPEED_MOVE_Y_POSITION);
        }
        if (clueActive != null)
        {
            await UniTask.Delay(500);

            var recTransformClueActive = clueActive.GetComponent<RectTransform>();
            recTransformClueActive.DOAnchorPos(START_Y_POSITION, SPEED_MOVE_Y_POSITION);
        }

        clueNoActive?.SetActive(false);
    }

    private void OnDestroy()
    {
        ManagerUniRx.Dispose(OnEducationEnd);
    }
}
