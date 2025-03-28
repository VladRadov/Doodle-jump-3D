using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class PlatformController
{
    private List<PlatformView> _platformsPrefab;
    private List<PlatformView> _platforms;
    private List<PlatformView> _selectPlatfroms;
    private PlatformView _currentSelectPlatfrom;
    private CancellationTokenSource _tonekCancelOutilePlatforms;

    private int _countStartPlatform;
    private float _offsetX;
    private float _offsetY;
    private float _offsetZ;
    private float _offsetXFrameMap;
    private float _minDistanceSelect;

    private const int START_POSITION_Z = 5;
    private const int MAX_COUNT_PLATFORM_OF_FRAME = 10;

    public PlatformController(List<PlatformView> platformsPrefab, int countStartPlatform)
    {
        _platforms = new List<PlatformView>();
        _selectPlatfroms = new List<PlatformView>();
        _platformsPrefab = platformsPrefab;
        _countStartPlatform = countStartPlatform;
        _tonekCancelOutilePlatforms = new CancellationTokenSource();
    }

    private Vector3 LastPositionPlatform => _platforms.Count != 0 ? _platforms[_platforms.Count - 1].transform.position : Vector3.zero;
    public PlatformView CurrentSelectPlatfrom => _currentSelectPlatfrom;

    public void Initialize(float offsetX, float offsetY, float offsetZ, float minDistnceSelect, float offsetXFrameMap, PlatformView startPlatform)
    {
        _offsetX = offsetX;
        _offsetY = offsetY;
        _offsetZ = offsetZ;
        _minDistanceSelect = minDistnceSelect;
        _offsetXFrameMap = offsetXFrameMap;
        _currentSelectPlatfrom = startPlatform;
    }

    public void Spawner()
    {
        for (int i = 0; i < _countStartPlatform; i++)
        {
            var countPlatform = Random.Range(1, MAX_COUNT_PLATFORM_OF_FRAME);
            for (int j = 0; j < countPlatform; j++)
            {
                var indexPlatform = Random.Range(0, _platformsPrefab.Count);
                var platform = PoolObjects<PlatformView>.GetObject(_platformsPrefab[indexPlatform]);

                var x = Random.Range(-_offsetX, _offsetX);
                var y = 0;//Random.Range(1, _offsetY);
                var z = Random.Range(START_POSITION_Z, _offsetZ);

                var nextPosition = new Vector3(x + _offsetXFrameMap, LastPositionPlatform.y + y, LastPositionPlatform.z + z);
                platform.SetPosition(nextPosition);
                platform.SetActiveOutline(false);

                _platforms.Add(platform);
            }
        }
    }

    public void FormationSelectionAllowedPlatform()
    {
        UseCancelToken();
        ClearSelectPlatforms();

        for (int i = 0; i < _platforms.Count; i++)
        {
            var isPlatformNext = Mathf.Abs(_currentSelectPlatfrom.transform.position.z) < Mathf.Abs(_platforms[i].transform.position.z);
            var isPositionPlatformNearDoodle = Mathf.Abs(_currentSelectPlatfrom.transform.position.z - _platforms[i].transform.position.z) < _minDistanceSelect;

            if (isPlatformNext && isPositionPlatformNearDoodle)
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

                if (isPlatformHave && _selectPlatfroms[i].IsDoodleOnPlatform == false)
                {
                    _currentSelectPlatfrom = _selectPlatfroms[i];
                    _currentSelectPlatfrom.SetActiveOutline(true);
                    await UniTask.Delay(500, cancellationToken: _tonekCancelOutilePlatforms.Token);
                    _currentSelectPlatfrom.SetActiveOutline(false);
                }
            }
        }
    }

    private void ClearSelectPlatforms()
        => _selectPlatfroms.Clear();

    private void UseCancelToken()
    {
        _tonekCancelOutilePlatforms.Cancel(false);
        _tonekCancelOutilePlatforms.Dispose();
        _tonekCancelOutilePlatforms = new CancellationTokenSource();
    }
}
