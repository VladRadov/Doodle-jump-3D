using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using Cysharp.Threading.Tasks;
using UniRx;

public class PlatformController
{
    private List<PlatformView> _platformsPrefab;
    private List<PlatformView> _platforms;
    private List<PlatformView> _selectPlatfroms;
    private ManagerFramesMap _managerFramesMap;
    private PlatformView _currentSelectPlatfrom;
    private CancellationTokenSource _tonekCancelOutlinePlatforms;

    private int _countStartPlatform;
    private float _offsetX;
    private float _offsetY;
    private float _offsetZ;
    private float _minDistanceSelect;

    private const int START_POSITION_Z = 5;
    private const int MAX_COUNT_PLATFORM_OF_FRAME = 5;

    public PlatformController(List<PlatformView> platformsPrefab, int countStartPlatform)
    {
        _platforms = new List<PlatformView>();
        _selectPlatfroms = new List<PlatformView>();
        _platformsPrefab = platformsPrefab;
        _countStartPlatform = countStartPlatform;
        _tonekCancelOutlinePlatforms = new CancellationTokenSource();
    }

    public PlatformView CurrentSelectPlatfrom => _currentSelectPlatfrom;

    public void Initialize(float offsetX, float offsetY, float offsetZ, float minDistnceSelect, PlatformView startPlatform, ManagerFramesMap managerFramesMap)
    {
        _offsetX = offsetX;
        _offsetY = offsetY;
        _offsetZ = offsetZ;
        _minDistanceSelect = minDistnceSelect;
        _currentSelectPlatfrom = startPlatform;
        _managerFramesMap = managerFramesMap;
    }

    public void SpawnerPlatforms()
    {
        for (int k = 0; k < _managerFramesMap.FramesMapViews.Count; k++)
        {
            Vector3 lastPositionPlatform = Vector3.zero;
            for (int i = 0; i < _countStartPlatform; i++)
            {
                var countPlatform = Random.Range(1, MAX_COUNT_PLATFORM_OF_FRAME);
                for (int j = 0; j < countPlatform; j++)
                {
                    var x = Random.Range(-_offsetX, _offsetX);
                    var y = lastPositionPlatform.y;
                    var z = k != 0 && i == 0 && j == 0 ? 0 : lastPositionPlatform.z + Random.Range(START_POSITION_Z, _offsetZ);

                    if (Mathf.Abs(z) > Mathf.Abs(_managerFramesMap.FramesMapViews[k].Tail.localPosition.z))
                        continue;

                    lastPositionPlatform = CreatePlatform(new Vector3(x, y, z), _managerFramesMap.FramesMapViews[k].transform);

                    //var indexPlatform = Random.Range(0, _platformsPrefab.Count);
                    //var platform = PoolObjects<PlatformView>.GetObject(_platformsPrefab[indexPlatform], _managerFramesMap.FramesMapViews[k].transform);

                    //var nextPosition = new Vector3(x, y, z);
                    //platform.SetLocalPosition(nextPosition);
                    //platform.SetActiveOutline(false);

                    //lastPositionPlatform = platform.transform.localPosition;
                    //_platforms.Add(platform);
                }
            }
        }
    }

    public void RespawnPlatforms(FrameMapView frameMapView)
    {
        Vector3 lastPositionPlatform = Vector3.zero;
        for (int i = 0; i < _countStartPlatform; i++)
        {
            var countPlatform = Random.Range(1, MAX_COUNT_PLATFORM_OF_FRAME);
            for (int j = 0; j < countPlatform; j++)
            {
                var x = i == 0 && j == 0 ? 0 : Random.Range(-_offsetX, _offsetX);
                var y = lastPositionPlatform.y;
                var z = i == 0 && j == 0 ? 0 : lastPositionPlatform.z + Random.Range(START_POSITION_Z, _offsetZ);

                if (Mathf.Abs(z) > Mathf.Abs(frameMapView.Tail.localPosition.z))
                    continue;

                lastPositionPlatform = CreatePlatform(new Vector3(x, y, z), frameMapView.transform);

                //var indexPlatform = Random.Range(0, _platformsPrefab.Count);
                //var platform = PoolObjects<PlatformView>.GetObject(_platformsPrefab[indexPlatform]);

                //var nextPosition = new Vector3(x, y, z);
                //platform.transform.parent = frameMapView.transform;
                //platform.SetLocalPosition(nextPosition);
                //platform.SetActiveOutline(false);

                //if (_platforms.Contains(platform) == false)
                //    _platforms.Add(platform);

                //lastPositionPlatform = platform.transform.localPosition;
            }
        }
    }

    public void NoActiveOldPlatforms(FrameMapView frameMapView)
    {
        for (int i = 0; i < _platforms.Count; i++)
        {
            var isPlatformOnFramemap = _platforms[i].transform.parent == frameMapView.transform;

            if (isPlatformOnFramemap)
                _platforms[i].SetActive(false);
        }
    }

    public void FormationSelectionAllowedPlatform()
    {
        UseCancelToken();
        ClearSelectPlatforms();

        for (int i = 0; i < _platforms.Count; i++)
        {
            var isActivePlatform = _platforms[i].gameObject.activeSelf;
            var isPlatformNext = Mathf.Abs(_currentSelectPlatfrom.transform.position.z) < Mathf.Abs(_platforms[i].transform.position.z);
            var isPositionPlatformNearDoodle = Mathf.Abs(_currentSelectPlatfrom.transform.position.z - _platforms[i].transform.position.z) <= _minDistanceSelect;
            var isNoCurrentPlatform = _platforms[i].IsDoodleOnPlatform == false;

            if (isActivePlatform && isPlatformNext && isPositionPlatformNearDoodle && isNoCurrentPlatform)
                _selectPlatfroms.Add(_platforms[i]);
        }

        OutlineSelectionAllowedPlatform();
    }

    public async UniTaskVoid OutlineSelectionAllowedPlatform()
    {
        while (_selectPlatfroms.Count != 0)
        {
            for (int i = 0; i < _selectPlatfroms.Count; i++)
            {
                var isPlatformHave = _selectPlatfroms.Count != 0 && i < _selectPlatfroms.Count;

                if (isPlatformHave)
                {
                    _currentSelectPlatfrom = _selectPlatfroms[i];
                    _currentSelectPlatfrom.ActiveOutlineColor(_currentSelectPlatfrom.ColorDefault);
                    await UniTask.Delay(500, cancellationToken: _tonekCancelOutlinePlatforms.Token);
                    _currentSelectPlatfrom.SetActiveOutline(false);
                }
            }
        }
    }

    private Vector3 CreatePlatform(Vector3 positionPlatform, Transform parent)
    {
        var indexPlatform = Random.Range(0, _platformsPrefab.Count);
        var platform = PoolObjects<PlatformView>.GetObject(_platformsPrefab[indexPlatform]);

        var nextPosition = positionPlatform;
        platform.transform.parent = parent;
        platform.SetLocalPosition(nextPosition);
        platform.SetActiveOutline(false);

        if (_platforms.Contains(platform) == false)
        {
            _platforms.Add(platform);
            platform.OnCollisionMap.Subscribe(platformView => { RespawnPlatform(platformView); });
        }

        return platform.transform.localPosition;
    }

    private void RespawnPlatform(PlatformView platform)
    {
        var x = Random.Range(-_offsetX, _offsetX);
        var y = platform.transform.position.y;
        var z = platform.transform.position.z;

        platform.SetLocalPosition(new Vector3(x, y, z));
        platform.SetActive(true);
    }

    private void ClearSelectPlatforms()
        => _selectPlatfroms.Clear();

    private void UseCancelToken()
    {
        _tonekCancelOutlinePlatforms.Cancel(false);
        _tonekCancelOutlinePlatforms.Dispose();
        _tonekCancelOutlinePlatforms = new CancellationTokenSource();
    }
}
