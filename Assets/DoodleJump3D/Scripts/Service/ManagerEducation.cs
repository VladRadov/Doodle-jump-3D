using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class ManagerEducation : BaseManager
{
    [SerializeField] private GameObject _parentPanel;
    [SerializeField] private ClueView _clue1;
    [SerializeField] private ClueView _clue2;
    [SerializeField] private ClueView _clue3;

    public ReactiveCommand OnEducationEnd = new();

    public override void Initialize()
    {
        _clue1.OnOkClick.Subscribe(_ =>
        {
            _clue1.SetActive(false);
            _clue2.SetActive(true);
        });

        _clue2.OnOkClick.Subscribe(_ =>
        {
            _clue2.SetActive(false);
            _clue3.SetActive(true);
        });

        _clue3.OnOkClick.Subscribe(_ =>
        {
            _clue3.SetActive(false);
            OnEducationEnd.Execute();
        });

        ManagerUniRx.AddObjectDisposable(OnEducationEnd);
    }

    public void SetActive(bool value)
        => _parentPanel.gameObject.SetActive(value);

    private void OnDestroy()
    {
        ManagerUniRx.Dispose(OnEducationEnd);
    }
}
