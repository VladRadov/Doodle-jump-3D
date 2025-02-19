using UnityEngine;

using UniRx;

public class InputComponent : BaseComponent
{
    private DoodleInputSystem _playerInput;

    public ReactiveCommand<Vector2> InputCommand = new();
    public ReactiveCommand JumpCommand = new();
    public ReactiveCommand ShootingCommand = new();

    private void Awake()
    {
        _playerInput = new DoodleInputSystem();

        _playerInput.Doodle.Jump.started += JumpStarted;
        _playerInput.Doodle.Shot.started += ShotStarted;

        ManagerUniRx.AddObjectDisposable(InputCommand);
        ManagerUniRx.AddObjectDisposable(JumpCommand);
        ManagerUniRx.AddObjectDisposable(ShootingCommand);
    }

    private void ShotStarted(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        ShootingCommand.Execute();
    }

    private void JumpStarted(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        JumpCommand.Execute();
    }

    private void OnEnable()
    {
        _playerInput.Enable();
    }

    private void OnDisable()
    {
        _playerInput.Disable();
    }

    private void FixedUpdate()
    {
        var value = _playerInput.Doodle.Move.ReadValue<Vector2>();
        InputCommand.Execute(value);
    }

    private void OnDestroy()
    {
        ManagerUniRx.Dispose(InputCommand);
        ManagerUniRx.Dispose(JumpCommand);
        ManagerUniRx.Dispose(ShootingCommand);
    }
}
