using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FramesMapController
{
    private List<FrameMapView> _framesMapViews;
    private FrameMapView _lastFrameMap;
    private Vector3 _offsetFrameMap;
    private float _deltaPositionRespawn;

    public FramesMapController(List<FrameMapView> framesMapViews, FrameMapView lastFrameMap, Vector3 offsetFrameMap, float deltaPositionRespawn)
    {
        _framesMapViews = framesMapViews;
        _lastFrameMap = lastFrameMap;
        _offsetFrameMap = offsetFrameMap;
        _deltaPositionRespawn = deltaPositionRespawn;
    }

    public void CheckAndRespawnFramesMap(Vector3 positionDoodle)
    {
        for (int i = 0; i < _framesMapViews.Count; i++)
        {
            if (Mathf.Abs(_framesMapViews[i].Tail.position.z + _deltaPositionRespawn) < Mathf.Abs(positionDoodle.z))
            {
                _framesMapViews[i].SetPosition(_lastFrameMap.transform.position + _offsetFrameMap);
                _lastFrameMap = _framesMapViews[i];
            }
        }
    }
}
