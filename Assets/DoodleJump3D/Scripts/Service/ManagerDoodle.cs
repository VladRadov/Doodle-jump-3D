using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerDoodle : BaseManager
{
    private DoodleController _doodleController;

    [SerializeField] private DoodleView _doodleView;
    [SerializeField] private DoodleAnimator _doodleAnimator;

    public DoodleController DoodleController => _doodleController;
    public DoodleAnimator DoodleAnimator => _doodleAnimator;
    public DoodleView DoodleView => _doodleView;

    public override void Initialize()
    {
        _doodleController = new DoodleController(_doodleView, new Doodle());
    }

    private void OnValidate()
    {
        if (_doodleAnimator == null)
            _doodleAnimator = GetComponent<DoodleAnimator>();
    }
}
