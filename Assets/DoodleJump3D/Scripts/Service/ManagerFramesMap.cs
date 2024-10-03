using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerFramesMap : BaseManager
{
    private FramesMapController _framesMapController;

    [SerializeField] private List<FrameMapView> _framesMapViews;
    [SerializeField] private FrameMapView _lastFrameMap;
    [SerializeField] private Vector3 _offsetFrameMap;
    [SerializeField] private float _deltaPositionRespawn;

    public FramesMapController FramesMapController => _framesMapController;

    public override void Initialize()
    {
        _framesMapController = new FramesMapController(_framesMapViews, _lastFrameMap, _offsetFrameMap, _deltaPositionRespawn);
    }
}
