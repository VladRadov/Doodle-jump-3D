public class InputPC : BaseInput
{
    public override void Initialize()
    {
        base.Initialize();

        _playerInput.Doodle.Jump.started += JumpStarted;
        _playerInput.Doodle.Shot.started += ShotStarted;
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
