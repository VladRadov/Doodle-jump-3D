using System.Collections.Generic;
using UnityEngine;

public class ManagerPlatform : BaseManager
{
    [SerializeField] private List<PlatformView> _platformsPrefab;
    [SerializeField] private PlatformView _startPlatform;
    [SerializeField] private ManagerFramesMap _managerFramesMap;

    [SerializeField] private int _countStartPlatform;
    [SerializeField] private float _offsetX;
    [SerializeField] private float _offsetY;
    [SerializeField] private float _offsetZ;
    [SerializeField] private float _minDistnceSelect;
    [SerializeField] private Color _colorEntryPlatfrom;

    private PlatformController _platformController;

    public PlatformController PlatformController => _platformController;

    public override void Initialize()
    {
        _platformController = new PlatformController(_platformsPrefab, _countStartPlatform);
        _platformController.Initialize(_offsetX, _offsetY, _offsetZ, _minDistnceSelect, _startPlatform, _managerFramesMap);
        _platformController.SpawnerPlatforms();
        _platformController.FormationSelectionAllowedPlatform();
        _platformController.OutlineSelectionAllowedPlatform();
    }

    public void OutlineNextPlatform()
        => _platformController.NextSelectPlatfrom.ActiveOutlineColor(_colorEntryPlatfrom);

    private void OnDestroy()
    {
        if(_platformController != null)
            _platformController.Dispose();
    }
}
