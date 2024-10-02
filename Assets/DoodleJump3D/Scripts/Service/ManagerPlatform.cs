using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerPlatform : BaseManager
{
    [SerializeField] private List<PlatformView> _platformsPrefab;
    [SerializeField] private PlatformView _startPlatform;

    [SerializeField] private int _countStartPlatform;
    [SerializeField] private float _offsetX;
    [SerializeField] private float _offsetY;
    [SerializeField] private float _offsetZ;
    [SerializeField] private float _minDistnceSelect;
    [SerializeField] private float _offsetXFrameMap;
    [SerializeField] private Color _colorEntryPlatfrom;

    private PlatformController _platformController;

    public PlatformController PlatformController => _platformController;

    public override void Initialize()
    {
        _platformController = new PlatformController(_platformsPrefab, _countStartPlatform);
        _platformController.Initialize(_offsetX, _offsetY, _offsetZ, _minDistnceSelect, _offsetXFrameMap, _startPlatform);
        _platformController.Spawner();
        _platformController.FormationSelectionAllowedPlatform();
    }

    public void OutlineCurrentPlatform()
    {
        _platformController.CurrentSelectPlatfrom.ActiveOutlineEntry(_colorEntryPlatfrom);
    }
}
