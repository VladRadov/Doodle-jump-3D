using UnityEngine;

using UniRx;

public class BaseInput : MonoBehaviour
{
    protected DoodleInputSystem _playerInput;

    public ReactiveCommand<Vector2> InputCommand = new();
    public ReactiveCommand JumpCommand = new();
    public ReactiveCommand ShootingCommand = new();

    public virtual void Initialize()
    {
        _playerInput = new DoodleInputSystem();

        ManagerUniRx.AddObjectDisposable(InputCommand);
        ManagerUniRx.AddObjectDisposable(JumpCommand);
        ManagerUniRx.AddObjectDisposable(ShootingCommand);
    }

    public virtual void FixedUpdate()
    {

    }

    public virtual void OnEnable()
    {
        if(_playerInput != null)
            _playerInput.Enable();
    }

    public void OnDisable()
    {
        if (_playerInput != null)
            _playerInput.Disable();
    }

    public void OnDestroy()
    {
        ManagerUniRx.Dispose(InputCommand);
        ManagerUniRx.Dispose(JumpCommand);
        ManagerUniRx.Dispose(ShootingCommand);
    }
}
