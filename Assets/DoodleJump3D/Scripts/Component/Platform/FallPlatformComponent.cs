using UnityEngine;

using UniRx;

public class FallPlatformComponent : BaseComponent
{
    [SerializeField] private ManagerPlatformBrownAnimator _managerPlatformBrownAnimator;
    [SerializeField] private PlatformView _platformView;

    public ReactiveCommand FallPlatformCommand = new();

    public override void Start()
    {
        base.Start();
        _platformView.OnCollisionDoodle.Subscribe(_ => { FallPlatform(); });
        ManagerUniRx.AddObjectDisposable(FallPlatformCommand);
    }

    private void FallPlatform()
    {
        _managerPlatformBrownAnimator.PlayRotation();
        _platformView.OnGravity();
        FallPlatformCommand.Execute();
    }

    private void OnDisable()
    {
        _managerPlatformBrownAnimator.SetDefaultState();
        transform.rotation = Quaternion.Euler(0, 0, 0);
        _platformView.OffGravity();
    }

    private void OnValidate()
    {
        if (_managerPlatformBrownAnimator == null)
            _managerPlatformBrownAnimator = GetComponent<ManagerPlatformBrownAnimator>();

        if (_platformView == null)
            _platformView = GetComponent<PlatformView>();
    }

    private void OnDestroy()
    {
        ManagerUniRx.Dispose(FallPlatformCommand);
    }
}
