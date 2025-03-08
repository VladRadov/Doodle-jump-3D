using UnityEngine;

public class InputPC : BaseInput
{
    public override void Initialize()
    {
        base.Initialize();

        _playerInput.Doodle.Jump.started += JumpStarted;
        _playerInput.Doodle.Shot.started += ShotStarted;
    }

    public override void FixedUpdate()
    {
        if (_playerInput != null)
        {
            var value = _playerInput.Doodle.Move.ReadValue<Vector2>();
            InputCommand.Execute(value);
        }
    }

    private void ShotStarted(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        ShootingCommand.Execute();
    }

    private void JumpStarted(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        JumpCommand.Execute();
    }
}
