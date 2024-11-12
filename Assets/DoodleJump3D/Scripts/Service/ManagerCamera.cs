using UnityEngine;

public class ManagerCamera : BaseManager
{
    [SerializeField] private ShakeCamera _shakeCamera;

    public ShakeCamera ShakeCamera => _shakeCamera;

    public override void Initialize()
    {

    }
}
