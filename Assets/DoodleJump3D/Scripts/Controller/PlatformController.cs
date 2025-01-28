using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;
using Cysharp.Threading.Tasks;
using UniRx;

public class PlatformController
{
    private List<PlatformView> _platformsPrefab;
    private List<PlatformView> _platforms;
    private List<PlatformView> _selectPlatforms;
    private ManagerFramesMap _managerFramesMap;
    private PlatformView _previousSelectedPlatfrom;
    private PlatformView _currentSelectPlatfrom;
    private PlatformView _nextSelectPlatfrom;
    private CancellationTokenSource _tonekCancelOutlinePlatforms;

    private int _countStartPlatform;
    private float _offsetX;
    private float _offsetY;
    private float _offsetZ;
    private float _minDistanceSelect;

    private const int START_POSITION_Z = 6;
    private const int MAX_COUNT_PLATFORM_OF_FRAME = 5;

    public PlatformController(List<PlatformView> platformsPrefab, int countStartPlatform)
    {
        _platforms = new List<PlatformView>();
        _selectPlatforms = new List<PlatformView>();

        _platformsPrefab = platformsPrefab;
        _countStartPlatform = countStartPlatform;

        _tonekCancelOutlinePlatforms = new CancellationTokenSource();

        ManagerUniRx.AddObjectDisposable(StartRespawnPlatformCommand);
        ManagerUniRx.AddObjectDisposable(ExplodingPlatformCommand);
        ManagerUniRx.AddObjectDisposable(FallPlatformCommand);
        ManagerUniRx.AddObjectDisposable(HidePlatformCommand);
    }

    public PlatformView PreviousSelectedPlatfrom => _previousSelectedPlatfrom;
    public PlatformView CurrentSelectPlatfrom => _currentSelectPlatfrom;
    public PlatformView NextSelectPlatfrom => _nextSelectPlatfrom;
    public UnityEvent<Transform, int> RespawnPlatformEventHandler = new();
    public ReactiveCommand<int> StartRespawnPlatformCommand = new();
    public ReactiveCommand ExplodingPlatformCommand = new();
    public ReactiveCommand FallPlatformCommand = new();
    public ReactiveCommand HidePlatformCommand = new();

    public void Initialize(float offsetX, float offsetY, float offsetZ, float minDistnceSelect, PlatformView startPlatform, ManagerFramesMap managerFramesMap)
    {
        _offsetX = offsetX;
        _offsetY = offsetY;
        _offsetZ = offsetZ;
        _minDistanceSelect = minDistnceSelect;
        _currentSelectPlatfrom = startPlatform;
        _managerFramesMap = managerFramesMap;
    }

    public void SetCurrentSelectPlatfrom(PlatformView platformView)
        => _currentSelectPlatfrom = platformView;

    public void SetNextSelectPlatfrom(PlatformView platformView)
        => _nextSelectPlatfrom = platformView;

    public void SetPreviousSelectedPlatfrom(PlatformView platformView)
        => _previousSelectedPlatfrom = platformView;

    public void SpawnerPlatforms()
    {
        for (int k = 0; k < _managerFramesMap.FramesMapViews.Count; k++)
        {
            Vector3 lastLocalPositionPlatform = Vector3.zero;
            for (int i = 0; i < _countStartPlatform; i++)
            {
                var countPlatform = Random.Range(1, MAX_COUNT_PLATFORM_OF_FRAME);
                for (int j = 0; j < countPlatform; j++)
                {
                    var x = Random.Range(-_offsetX, _offsetX);
                    var y = lastLocalPositionPlatform.y;
                    var z = k != 0 && i == 0 && j == 0 ? 0 : lastLocalPositionPlatform.z + Random.Range(START_POSITION_Z, _offsetZ);

                    if (Mathf.Abs(z) > Mathf.Abs(_managerFramesMap.FramesMapViews[k].Tail.localPosition.z))
                        continue;

                    var platform = CreatePlatform(new Vector3(x, y, z), _managerFramesMap.FramesMapViews[k].transform);
                    lastLocalPositionPlatform = platform.transform.localPosition;
                }
            }
        }
    }

    public void RespawnPlatforms(FrameMapView frameMapView)
    {
        StartRespawnPlatformCommand.Execute(_countStartPlatform);

        Vector3 lastLocalPositionPlatform = Vector3.zero;
        PlatformView platform = null;

        for (int i = 0; i < _countStartPlatform; i++)
        {
            var countPlatform = Random.Range(1, MAX_COUNT_PLATFORM_OF_FRAME);
            for (int j = 0; j < countPlatform; j++)
            {
                var x = i == 0 && j == 0 ? 0 : Random.Range(-_offsetX, _offsetX);
                var y = lastLocalPositionPlatform.y;
                var z = i == 0 && j == 0 ? 0 : lastLocalPositionPlatform.z + Random.Range(START_POSITION_Z, _offsetZ);

                if (Mathf.Abs(z) > Mathf.Abs(frameMapView.Tail.localPosition.z))
                    continue;

                platform = CreatePlatform(new Vector3(x, y, z), frameMapView.transform);
                lastLocalPositionPlatform = platform.transform.localPosition;
            }

            RespawnPlatformEventHandler.Invoke(platform.transform, i);
        }
    }

    public void NoActiveOldPlatforms(FrameMapView frameMapView)
    {
        for (int i = 0; i < _platforms.Count; i++)
        {
            var isPlatformOnFrameMap = _platforms[i].transform.parent == frameMapView.transform;

            if (isPlatformOnFrameMap)
                _platforms[i].SetActive(false);
        }
    }

    public void FormationSelectionAllowedPlatform()
    {
        UseCancelToken();
        ClearSelectPlatforms();

        float minimalDistanceNextToPlatform = -1;
        PlatformView platformHope = null;

        for (int i = 0; i < _platforms.Count; i++)
        {
            var distanceNextToPlatform = Mathf.Abs(_currentSelectPlatfrom.transform.position.z - _platforms[i].transform.position.z);

            var isActivePlatform = _platforms[i].gameObject.activeSelf;
            var isPlatformNext = Mathf.Abs(_currentSelectPlatfrom.transform.position.z) < Mathf.Abs(_platforms[i].transform.position.z);
            var isPositionPlatformNearDoodle = distanceNextToPlatform <= _minDistanceSelect;
            var isNoCurrentPlatform = _platforms[i].IsDoodleOnPlatform == false;

            if (isActivePlatform && isPlatformNext && isPositionPlatformNearDoodle && isNoCurrentPlatform)
                _selectPlatforms.Add(_platforms[i]);
            
            if ((minimalDistanceNextToPlatform == -1 || minimalDistanceNextToPlatform > distanceNextToPlatform) && isActivePlatform && isPlatformNext && isNoCurrentPlatform)
            {
                minimalDistanceNextToPlatform = distanceNextToPlatform;
                platformHope = _platforms[i];
            }
        }

        if (_selectPlatforms.Count == 0)
            _selectPlatforms.Add(platformHope);
    }

    public async UniTaskVoid OutlineSelectionAllowedPlatform()
    {
        while (_selectPlatforms.Count != 0)
        {
            for (int i = 0; i < _selectPlatforms.Count; i++)
            {
                var isPlatformHave = _selectPlatforms.Count != 0 && i < _selectPlatforms.Count;

                if (isPlatformHave)
                {
                    _nextSelectPlatfrom = _selectPlatforms[i];

                    if (_nextSelectPlatfrom == null)
                        return;

                    _nextSelectPlatfrom?.ActiveOutlineColor(_nextSelectPlatfrom.ColorDefault);
                    await UniTask.Delay(500, cancellationToken: _tonekCancelOutlinePlatforms.Token);
                    _nextSelectPlatfrom?.SetActiveOutline(false);
                }
            }
        }
    }

    public void ClearSlectePlatforms()
        => _selectPlatforms.Clear();

    public void ShiftRankPlatforams()
    {
        SetPreviousSelectedPlatfrom(_currentSelectPlatfrom);
        SetCurrentSelectPlatfrom(_nextSelectPlatfrom);
    }

    public PlatformView FindNearPlatformFromDoodle(Vector3 doodlePosition)
    {
        float minimalDistanceNextToPlatform = -1;
        PlatformView platformHope = null;

        for (int i = 0; i < _platforms.Count; i++)
        {
            var distanceNextToPlatform = Mathf.Abs(doodlePosition.z - _platforms[i].transform.position.z);

            var isActivePlatform = _platforms[i].gameObject.activeSelf;
            var isPlatformNext = Mathf.Abs(doodlePosition.z) < Mathf.Abs(_platforms[i].transform.position.z);

            if (isActivePlatform && isPlatformNext && (minimalDistanceNextToPlatform == -1 || minimalDistanceNextToPlatform > distanceNextToPlatform))
            {
                minimalDistanceNextToPlatform = distanceNextToPlatform;
                platformHope = _platforms[i];
            }
        }

        return platformHope;
    }

    public void Dispose()
    {
        ManagerUniRx.Dispose(StartRespawnPlatformCommand);
        ManagerUniRx.Dispose(ExplodingPlatformCommand);
        ManagerUniRx.Dispose(FallPlatformCommand);
        ManagerUniRx.Dispose(HidePlatformCommand);
    }

    private PlatformView CreatePlatform(Vector3 positionPlatform, Transform parent)
    {
        var indexPlatform = Random.Range(0, _platformsPrefab.Count);
        var platform = PoolObjects<PlatformView>.GetObject(_platformsPrefab[indexPlatform]);

        var explosionPlatformComponent = platform.GetComponent<ExplosionPlatformComponent>();
        if (explosionPlatformComponent != null)
        {
            explosionPlatformComponent.ExplodingPlatformCommand = new();
            explosionPlatformComponent.ExplodingPlatformCommand.Subscribe(_ => { ExplodingPlatformCommand.Execute(); });
        }

        var fallPlatformComponent = platform.GetComponent<FallPlatformComponent>();
        if (fallPlatformComponent != null)
        {
            fallPlatformComponent.FallPlatformCommand = new();
            fallPlatformComponent.FallPlatformCommand.Subscribe(_ => { FallPlatformCommand.Execute(); });
        }

        var invisibleComponent = platform.GetComponent<InvisibleComponent>();
        if (invisibleComponent != null)
        {
            invisibleComponent.HidePlatformCommand = new();
            invisibleComponent.HidePlatformCommand.Subscribe(_ => { HidePlatformCommand.Execute(); });
        }

        platform.transform.parent = parent;
        platform.SetLocalPosition(positionPlatform);
        platform.SetActiveOutline(false);

        if (_platforms.Contains(platform) == false)
        {
            _platforms.Add(platform);
            platform.OnCollisionTexture.Subscribe(platformView => { RespawnPlatform(platformView); });
        }

        return platform;
    }

    private void RespawnPlatform(PlatformView platform)
    {
        var x = Random.Range(-_offsetX, _offsetX);
        var y = 0;
        var z = platform.transform.localPosition.z;

        platform.SetLocalPosition(new Vector3(x, y, z));
        platform.SetActive(true);
    }

    private void ClearSelectPlatforms()
        => _selectPlatforms.Clear();

    private void UseCancelToken()
    {
        _tonekCancelOutlinePlatforms.Cancel(false);
        _tonekCancelOutlinePlatforms.Dispose();
        _tonekCancelOutlinePlatforms = new CancellationTokenSource();
    }
}
