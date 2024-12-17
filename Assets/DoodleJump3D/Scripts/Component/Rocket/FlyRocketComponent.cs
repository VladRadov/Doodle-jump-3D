using UnityEngine;

using UniRx;

public class FlyRocketComponent : MonoBehaviour
{
    private float _currentTime;
    private bool _isFlying;

    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private BoxCollider _boxCollider;
    [SerializeField] private Transform _backBraceBottom;
    [SerializeField] private Vector3 _speed;
    [SerializeField] private float _timeFlying;
    [SerializeField] private Vector3 _offsetConnectedDoodle;

    public ReactiveCommand FlyingCommand = new();
    public ReactiveCommand FlyingEndCommand = new();

    public void FlyRocket()
        => _rigidbody.AddForce(_speed * Time.deltaTime, ForceMode.VelocityChange);

    private void Start()
    {
        _isFlying = false;

        ManagerUniRx.AddObjectDisposable(FlyingCommand);
        ManagerUniRx.AddObjectDisposable(FlyingEndCommand);
    }

    private void Update()
    {
        if (_isFlying == false)
            return;

        if (_currentTime < _timeFlying)
            _currentTime += Time.deltaTime;
        else
        {
            FlyingEndCommand.Execute();
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Doodle"))
        {
            _boxCollider.isTrigger = false;
            _isFlying = true;

            other.transform.position = transform.position + _offsetConnectedDoodle;
            var jumpingComponent = other.GetComponent<JumpingComponent>();
            jumpingComponent.SetFlying(_rigidbody);

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
    }

    private void OnDestroy()
    {
        ManagerUniRx.Dispose(FlyingCommand);
        ManagerUniRx.Dispose(FlyingEndCommand);
    }
}
