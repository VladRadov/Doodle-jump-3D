using UnityEngine;

using UniRx;

public class FlyRocketComponent : MonoBehaviour
{
    private float _currentTime;
    private bool _isFlying;

    [Header("Components")]
    [SerializeField] private RocketView _rocketView;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private BoxCollider _boxCollider;
    [SerializeField] private Transform _backBraceBottom;
    [Header("Settings")]
    [SerializeField] private Vector3 _speed;
    [SerializeField] private float _timeFlying;
    [SerializeField] private Vector3 _offsetConnectedDoodle;
    [SerializeField] private Vector3 _rotateFlyingToRocket;

    public ReactiveCommand FlyingEndCommand = new();
    public bool IsFlying => _isFlying;

    public void FlyRocket()
    {
        transform.rotation = Quaternion.Euler(_rotateFlyingToRocket);
        _rigidbody.AddForce(_speed * Time.deltaTime, ForceMode.VelocityChange);
    }

    private void Start()
    {
        _isFlying = false;

        ManagerUniRx.AddObjectDisposable(FlyingEndCommand);
    }

    private void Update()
    {
        if (_isFlying == false)
            return;

        if (_currentTime < _timeFlying)
            _currentTime += Time.deltaTime;
        else
            FlyingEndCommand.Execute();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_isFlying == false && other.gameObject.layer == LayerMask.NameToLayer("Doodle"))
        {
            _boxCollider.isTrigger = false;
            _isFlying = true;

            var doodle = other.GetComponent<DoodleView>();
            transform.position = doodle.PointJointRocket;
            var jumpingComponent = other.GetComponent<JumpingComponent>();
            jumpingComponent.StartFlying(_rocketView);
            transform.parent = doodle.BaseTransform;

            FlyingEndCommand = new();
            FlyingEndCommand.Subscribe(_ =>
            {
                jumpingComponent.EndFlying();
                _boxCollider.isTrigger = true;
                _isFlying = false;
                _currentTime = 0;
            });

            FlyRocket();
        }
    }

    private void OnValidate()
    {
        if (_rigidbody == null)
            _rigidbody = GetComponent<Rigidbody>();

        if (_boxCollider == null)
            _boxCollider = GetComponent<BoxCollider>();

        if(_rocketView == null)
            _rocketView = GetComponent<RocketView>();
    }

    private void OnDestroy()
    {
        ManagerUniRx.Dispose(FlyingEndCommand);
    }
}
