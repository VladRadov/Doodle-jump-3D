using UnityEngine;
using UniRx;
using DG.Tweening;
using Cysharp.Threading.Tasks;

public class ManagerEducation : BaseManager
{
    private const float START_Y_POSITION = 600;
    private const float END_Y_POSITION = 350;
    private const float SPEED_MOVE_Y_POSITION = 0.3f;

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
            _clue4.transform.DOMoveY(START_Y_POSITION, SPEED_MOVE_Y_POSITION);
            await UniTask.Delay(500);

            _clue4.SetActive(false);
            OnEducationEnd.Execute();
        });

        ManagerUniRx.AddObjectDisposable(OnEducationEnd);
    }

    public void SetActive(bool value)
    {
        _parentPanel.gameObject.SetActive(value);
        _clue1.transform.DOMoveY(END_Y_POSITION, SPEED_MOVE_Y_POSITION);
    }

    private async void MoveCluePanel(ClueView clueNoActive, ClueView clueActive)
    {
        clueActive.SetActive(true);

        clueNoActive.transform.DOMoveY(START_Y_POSITION, SPEED_MOVE_Y_POSITION);
        await UniTask.Delay(500);
        clueActive.transform.DOMoveY(END_Y_POSITION, SPEED_MOVE_Y_POSITION);

        clueNoActive.SetActive(false);
    }

    private void OnDestroy()
    {
        ManagerUniRx.Dispose(OnEducationEnd);
    }
}
