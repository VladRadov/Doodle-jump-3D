using UnityEngine;

using Cinemachine;

public class ManagerCamera : BaseManager
{
    [SerializeField] private ShakeCamera _shakeCamera;
    [SerializeField] private CinemachineVirtualCamera _virtualCamera;

    public ShakeCamera ShakeCamera => _shakeCamera;

    public override void Initialize()
    {

    }

    public void ResetFollowCamera()
        => _virtualCamera.Follow = null;
}
