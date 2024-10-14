using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using UniRx;

public class FallPlatformComponent : BaseComponent
{
    [SerializeField] private ManagerPlatformBrownAnimator _managerPlatformBrownAnimator;
    [SerializeField] private PlatformView _platformView;

    public override void Start()
    {
        base.Start();
        _platformView.OnCollisionDoodle.Subscribe(_ => { FallPlatform(); });
    }

    private void FallPlatform()
    {
        _managerPlatformBrownAnimator.PlayRotation();
        _platformView.OnGravity();
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
}
