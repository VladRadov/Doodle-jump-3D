using System;
using System.Collections;
using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;

public class JumpingComponent : BaseComponent
{
    private Rigidbody _rigidbody;
    private bool _isJumpOnPlace;
    private bool _isJumpForward;
    private Vector3 _targetPlatform;

    private float _vX;
    private float _vY;
    private float _vZ;

    [SerializeField] private float _speedJump;
    [SerializeField] private float _angle;
    [SerializeField] private float _gravity;
    [SerializeField] private float _minLenghtToRotationDoodle;

    public ReactiveCommand<Vector3> JumpingOnPlaceCommnad = new();
    public ReactiveCommand<Vector3> JumpingOnForwardWithRotationCommnad = new();

    public void SetTargetPlatform(Vector3 target)
    {
        _isJumpForward = true;
        _targetPlatform = target;
    }

    public void JumpForward(PlatformView platformView)
    {
        Vector3 fromTo = _targetPlatform - transform.position;
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

        if(lengthVector > _minLenghtToRotationDoodle)
            JumpingOnForwardWithRotationCommnad.Execute(transform.position);
    }

    public override void Start()
    {
        base.Start();

        _isJumpOnPlace = true;
        _isJumpForward = false;

        ManagerUniRx.AddObjectDisposable(JumpingOnPlaceCommnad);
        ManagerUniRx.AddObjectDisposable(JumpingOnForwardWithRotationCommnad);
    }

    private void SetVelocity(Vector3 speed)
        => _rigidbody.velocity = speed;

    private void FixedUpdate()
    {
        if (_isJumpOnPlace)
        {
            _vX = 0;
            _vY = _speedJump;
            _vZ = 0;
            SetVelocity(new Vector3(_vX, _vY, _vZ));

            _isJumpOnPlace = false;
        }

        _vY -= _gravity * Time.deltaTime;
        SetVelocity(new Vector3(_rigidbody.velocity.x, _vY, _rigidbody.velocity.z));
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            JumpingOnPlaceCommnad.Execute(transform.position);

            if (_isJumpForward == false)
                _isJumpOnPlace = true;
            else
            {
                var platformView = collision.gameObject.GetComponent<PlatformView>();

                if (platformView != null)
                    JumpForward(platformView);
            }
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
        ManagerUniRx.Dispose(JumpingOnForwardWithRotationCommnad);
    }
}
