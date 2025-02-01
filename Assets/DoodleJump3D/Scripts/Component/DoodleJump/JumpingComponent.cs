using UnityEngine;
using UniRx;

public class JumpingComponent : BaseComponent
{
    private Rigidbody _rigidbody;
    private FixedJoint _fixedJoint;
    private Transform _transform;

    private bool _isJumpOnPlace;
    private bool _isJumpForward;
    private bool _isFlying;
    private bool _isAllowedToSide;

    private Vector3 _targetPlatform;

    private float _vX;
    private float _vY;
    private float _vZ;

    [SerializeField] private float _speedJump;
    [SerializeField] private float _angle;
    [SerializeField] private float _gravity;
    [SerializeField] private float _minLenghtToRotationDoodle;

    public ReactiveCommand<Vector3> JumpingOnPlaceCommnad = new();
    public ReactiveCommand JumpingOnForwardCommnad = new();
    public ReactiveCommand<Vector3> JumpingOnForwardWithRotationCommnad = new();
    public ReactiveCommand<RocketView> DoodleStartFlyingCommand = new();
    public ReactiveCommand<Vector3> DoodleEndFlyingCommand = new();
    public ReactiveCommand<Vector3> FlyingCommand = new();

    public bool IsFlying => _isFlying;
    public bool IsJumpOnPlace => _isJumpOnPlace;
    public bool IsAllowedToSide => _isAllowedToSide;

    public void StartFlying(RocketView rocket)
    {
        DoodleStartFlyingCommand.Execute(rocket);

        _fixedJoint = gameObject.AddComponent<FixedJoint>();
        _fixedJoint.connectedBody = rocket.Rigidbody;

        SetVelocity(Vector3.zero);
        _rigidbody.useGravity = false;
        _isFlying = true;
    }

    public void EndFlying()
    {
        if (_fixedJoint != null)
            Destroy(_fixedJoint);

        _rigidbody.useGravity = true;
        _isFlying = false;
        _isAllowedToSide = false;

        JumpingOnPlace();

        DoodleEndFlyingCommand.Execute(_transform.position);
        JumpingOnForwardWithRotationCommnad.Execute(_transform.position);
    }

    public void OnDieDoodle()
    {
        if (_fixedJoint != null)
            Destroy(_fixedJoint);

        _rigidbody.useGravity = true;
        _isFlying = false;
    }

    public void SetTargetPlatform(Vector3 target)
    {
        _isJumpForward = true;
        _targetPlatform = target;
    }

    public void JumpForward()
    {
        Vector3 fromTo = _targetPlatform - _transform.position;
        Vector3 fromToXZ = new Vector3(fromTo.x, 0, fromTo.z);

        float lengthVector = fromToXZ.magnitude;
        float angleInRadians = _angle * Mathf.PI / 180;

        float v0 = Mathf.Sqrt(Mathf.Abs((lengthVector * _gravity) / (Mathf.Sin(2 * angleInRadians))));
        _vY = v0 * Mathf.Sin(angleInRadians);
        float vX = v0 * Mathf.Cos(angleInRadians);

        _vX = fromTo.normalized.x * vX;
        _vY = v0 * Mathf.Sin(angleInRadians);
        _vZ = fromTo.normalized.z * vX;

        SetVelocity(new Vector3(_vX, _vY, _vZ));
        _isJumpForward = false;

        JumpingOnForwardCommnad.Execute();

        if(lengthVector > _minLenghtToRotationDoodle)
            JumpingOnForwardWithRotationCommnad.Execute(_transform.position);
    }

    public override void Start()
    {
        base.Start();

        _transform = transform;

        _isJumpOnPlace = true;
        _isJumpForward = false;
        _isAllowedToSide = true;

        ManagerUniRx.AddObjectDisposable(JumpingOnPlaceCommnad);
        ManagerUniRx.AddObjectDisposable(JumpingOnForwardCommnad);
        ManagerUniRx.AddObjectDisposable(JumpingOnForwardWithRotationCommnad);
        ManagerUniRx.AddObjectDisposable(DoodleStartFlyingCommand);
        ManagerUniRx.AddObjectDisposable(DoodleEndFlyingCommand);
        ManagerUniRx.AddObjectDisposable(FlyingCommand);
    }

    private void SetVelocity(Vector3 speed)
        => _rigidbody.velocity = speed;

    private void Update()
    {
        if (_isFlying)
        {
            FlyingCommand.Execute(_transform.position);
            return;
        }

        if (_isJumpOnPlace)
            JumpingOnPlace();

        _vY -= _gravity * Time.deltaTime;
        SetVelocity(new Vector3(_rigidbody.velocity.x, _vY, _rigidbody.velocity.z));
    }

    private void JumpingOnPlace()
    {
        _vX = 0;
        _vY = _speedJump;
        _vZ = 0;
        SetVelocity(new Vector3(_vX, _vY, _vZ));

        _isJumpOnPlace = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            _isAllowedToSide = true;

            JumpingOnPlaceCommnad.Execute(_transform.position);

            if (_isJumpForward == false)
                _isJumpOnPlace = true;
            else
                JumpForward();
        }
    }

    private void OnValidate()
    {
        if (_rigidbody == null)
            _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnDestroy()
    {
        ManagerUniRx.Dispose(JumpingOnPlaceCommnad);
        ManagerUniRx.Dispose(JumpingOnForwardCommnad);
        ManagerUniRx.Dispose(JumpingOnForwardWithRotationCommnad);
        ManagerUniRx.Dispose(DoodleStartFlyingCommand);
        ManagerUniRx.Dispose(DoodleEndFlyingCommand);
        ManagerUniRx.Dispose(FlyingCommand);
    }
}
