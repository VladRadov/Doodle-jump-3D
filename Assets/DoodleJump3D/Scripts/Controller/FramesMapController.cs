using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FramesMapController
{
    private List<FrameMapView> _framesMapViews;
    private FrameMapView _lastFrameMap;
    private Vector3 _offsetFrameMap;

    public FramesMapController(List<FrameMapView> framesMapViews, FrameMapView lastFrameMap, Vector3 offsetFrameMap)
    {
        _framesMapViews = framesMapViews;
        _lastFrameMap = lastFrameMap;
        _offsetFrameMap = offsetFrameMap;
    }

    public void CheckAndRespawnFramesMap(Vector3 positionDoodle)
    {
        for (int i = 0; i < _framesMapViews.Count; i++)
        {
            if (Mathf.Abs(_framesMapViews[i].Tail.position.z) < Mathf.Abs(positionDoodle.z))
            {
                _framesMapViews[i].SetPosition(_lastFrameMap.transform.position + _offsetFrameMap);
                _lastFrameMap = _framesMapViews[i];
            }
        }
    }
}
