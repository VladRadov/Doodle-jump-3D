using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class InputComponent : BaseComponent
{
    private DoodleInputSystem _playerInput;

    public ReactiveCommand<Vector2> InputCommand = new();
    public ReactiveCommand JumpCommand = new();

    private void Awake()
    {
        _playerInput = new DoodleInputSystem();
        _playerInput.Doodle.Jump.started += JumpStarted;
        ManagerUniRx.AddObjectDisposable(InputCommand);
        ManagerUniRx.AddObjectDisposable(JumpCommand);
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
    }
}
