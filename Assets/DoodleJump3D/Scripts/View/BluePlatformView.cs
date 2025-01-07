using UnityEngine;

using Cysharp.Threading.Tasks;

public class BluePlatformView : PlatformView
{
    private bool _isChangedRoute;

    [SerializeField] private MovablePlatformComponent _movablePlatformComponent;

    protected override void OnTriggerStay(Collider other)
    {
        if (_isChangedRoute == false)
        {
            _isChangedRoute = true;
            _movablePlatformComponent.ChangeRoute();
            ResetFlagChangedRoute();
        }
    }

    protected override void Start()
    {
        _isChangedRoute = false;

        base.Start();
    }

    private async void ResetFlagChangedRoute()
    {
        await UniTask.Delay(2000);
        _isChangedRoute = false;
    }

    private void OnValidate()
    {
        if (_movablePlatformComponent == null)
            _movablePlatformComponent = GetComponent<MovablePlatformComponent>();
    }
}
