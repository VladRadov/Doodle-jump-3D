using UnityEngine;

public class ManagerRocket : BaseManager
{
    private RocketsController _rocketsController;

    [SerializeField] private RocketView _rocketPrefab;
    [SerializeField] private SmokeEffectView _smokeEffectPrefab;

    public RocketsController Controller => _rocketsController;

    public override void Initialize()
    {
        _rocketsController = new RocketsController(_rocketPrefab, _smokeEffectPrefab);
    }
}
