using UnityEngine;
using UnityEngine.UI;

using UnityEngine.InputSystem;

public class InputMobile : BaseInput
{
    [SerializeField] private Button _shot;
    [SerializeField] private Button _jump;

    public override void Initialize()
    {
        base.Initialize();

        SetActiveButtons(true);

        _shot.onClick.AddListener(() => { ShootingCommand.Execute(); });
        _jump.onClick.AddListener(() => { JumpCommand.Execute(); });
    }

    public override void OnEnable()
    {
        if (_playerInput != null)
            _playerInput.Enable();

        if(Accelerometer.current != null)
            InputSystem.EnableDevice(Accelerometer.current);
    }

    public void Update()
    {
        if (Accelerometer.current != null)
        {
            var acceleration = Accelerometer.current.acceleration.ReadValue();
            InputCommand.Execute(new Vector2(acceleration.x, 0) * Time.deltaTime * 200f);
        }
    }

    public void SetActiveButtons(bool value)
    {
        _shot.gameObject.SetActive(value);
        _jump.gameObject.SetActive(value);
    }
}
