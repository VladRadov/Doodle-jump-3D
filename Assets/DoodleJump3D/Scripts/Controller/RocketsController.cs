using System.Collections.Generic;
using UnityEngine;

using Cysharp.Threading.Tasks;

public class RocketsController
{
    private RocketView _rocketPrefab;
    private SmokeEffectView _smokeEffectPrefab;
    private List<RocketView> _rockets;
    private RocketView _currentRocket;

    private int _indexPlatformForSpawn;
    private bool _isFlying;

    public RocketsController(RocketView rocketPrefab, SmokeEffectView smokeEffectPrefab)
    {
        _rocketPrefab = rocketPrefab;
        _smokeEffectPrefab = smokeEffectPrefab;
        _rockets = new List<RocketView>();
    }

    public void SetPositionCurrentRocket(Vector3 newPosition)
        => _currentRocket?.SetPosition(newPosition);

    public void SetCurrentRocket(RocketView rocketView)
        => _currentRocket = rocketView;

    public void NoActiveCurrentRocket()
        => _currentRocket?.SetActive(false);

    public void GetRandomIndexPlatformForSpawnRocket(int countStartPlatform)
        => _indexPlatformForSpawn = Random.Range(0, countStartPlatform);

    public void SpawnRocket(Transform parent, int indexCurrentPlatform)
    {
        if (indexCurrentPlatform == _indexPlatformForSpawn)
        {
            var rocket = PoolObjects<RocketView>.GetObject(_rocketPrefab, parent);
            rocket.transform.localPosition = new Vector3(0, 5, 0);

            AddRocket(rocket);
        }
    }

    public void SetFlagFlying(bool value)
        => _isFlying = value;

    public async void StartSmokeEffect(Transform doodleTransform)
    {
        while (_isFlying)
        {
            if (doodleTransform == null)
                break;

            var smokeEffect = PoolObjects<SmokeEffectView>.GetObject(_smokeEffectPrefab);
            smokeEffect.SetPosition(doodleTransform.position);
            await UniTask.Delay(100);
        }
    }

    public void ClearNoActiveRockets()
    {
        for (int i = 0; i < _rockets.Count; i++)
        {
            if(_rockets[i].transform.parent.gameObject.activeSelf == false)
                _rockets[i].SetActive(false);
        }
    }

    private void AddRocket(RocketView rocketView)
    {
        if(_rockets.Contains(rocketView) == false)
            _rockets.Add(rocketView);
    }
}
