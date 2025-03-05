using UnityEngine;

using RimuruDev;

public class InputManager : BaseManager
{
    private BaseInput _baseInput;

    [SerializeField] private DeviceTypeDetector _deviceTypeDetector;
    [SerializeField] private InputPC _inputPC;
    [SerializeField] private InputMobile _inputMobile;

    public BaseInput BaseInput => _baseInput;

    public override void Initialize()
    {
        if (_deviceTypeDetector.CurrentDeviceType == CurrentDeviceType.WebPC)
        {
            _baseInput = _inputPC;
            _inputMobile.SetActiveButtons(false);
        }
        else
            _baseInput = _inputMobile;

        _baseInput.Initialize();
        _baseInput.OnEnable();
    }

    public void FixedUpdate()
    {
        if(_baseInput != null)
            _baseInput.FixedUpdate();
    }

    public void OnEnable()
    {
        if (_baseInput != null)
            _baseInput.OnEnable();
    }

    public void OnDisable()
    {
        if (_baseInput != null)
            _baseInput.OnDisable();
    }

    public void OnDestroy()
    {
        if (_baseInput != null)
            _baseInput.OnDestroy();
    }
}
