using UnityEngine;

using Cysharp.Threading.Tasks;

public class ManagerRocket : BaseManager
{
    private bool _isFlying;

    [SerializeField] private RocketView _rocketPrefab;
    [SerializeField] private SmokeEffectView _smokeEffectPrefab;

    public override void Initialize()
    {

    }

    public void SpawnRocket(Transform parent)
    {
        var rocket = PoolObjects<RocketView>.GetObject(_rocketPrefab, parent);
        rocket.transform.localPosition = new Vector3(0, 5, 0);
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
}
