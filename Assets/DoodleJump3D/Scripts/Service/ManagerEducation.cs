using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class ManagerEducation : BaseManager
{
    [SerializeField] private ClueView _clue1;
    [SerializeField] private ClueView _clue2;

    public ReactiveCommand OnEducationEnd = new();

    public override void Initialize()
    {
        _clue1.OnOkClick.Subscribe(_ =>
        {
            _clue1.SetActive(false);
            _clue2.SetActive(true);
        });

        _clue2.OnOkClick.Subscribe(_ => { OnEducationEnd.Execute(); });

        ManagerUniRx.AddObjectDisposable(OnEducationEnd);
    }

    private void OnDestroy()
    {
        ManagerUniRx.Dispose(OnEducationEnd);
    }
}
