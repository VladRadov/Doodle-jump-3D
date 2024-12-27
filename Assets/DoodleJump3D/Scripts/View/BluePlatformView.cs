using UnityEngine;

public class BluePlatformView : PlatformView
{
    [SerializeField] private MovablePlatformComponent _movablePlatformComponent;

    protected override void OnTriggerEnter(Collider other)
    {
        _movablePlatformComponent.ChangeRoute();
    }

    private void OnValidate()
    {
        if (_movablePlatformComponent == null)
            _movablePlatformComponent = GetComponent<MovablePlatformComponent>();
    }
}
