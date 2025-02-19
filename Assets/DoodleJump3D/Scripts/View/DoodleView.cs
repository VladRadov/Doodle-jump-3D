using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

using UniRx;

public class DoodleView : MonoBehaviour
{
    private Transform _transform;
    private bool _isDie;
    private int _zPosition;

    [Header("Components")]
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private CapsuleCollider _capsuleCollider;
    [SerializeField] private List<BaseComponent> _components;
    [SerializeField] private Transform _pointJointRocket;
    [SerializeField] private SplineAnimate _splineAnimate;
    [SerializeField] private Animator _animator;
    [Header("Objects")]
    [SerializeField] private BoxStretcher _boxStretcher;
    [SerializeField] private RocketView rocketCatSceneView;
    [Header("Settings")]
    [SerializeField] private Vector3 _startPositionAfterCatScene;

    public List<BaseComponent> Components => _components;
    public ReactiveCommand<int> ChangingPosition = new();
    public ReactiveCommand DoodleDieCommand = new();
    public ReactiveCommand GetStarCommand = new();
    public ReactiveCommand<Transform> SplineAnimateStartCommand = new();
    public ReactiveCommand SplineAnimateEndCommand = new();

    public bool IsDie => _isDie;
    public Vector3 PointJointRocket => _pointJointRocket.position;

    public void ActiveTriggerCollider()
        => _capsuleCollider.isTrigger = true;

    public void SetPositionAfterCatScene()
        => transform.position = _startPositionAfterCatScene;

    public void OnSplineAnimateEnd()
    {
        _splineAnimate.enabled = false;
        _animator.enabled = true;
        _boxStretcher.gameObject.SetActive(true);
        rocketCatSceneView.gameObject.SetActive(false);
    }

    private void Start()
    {
        _transform = transform;
        _zPosition = (int)_transform.position.z;
        _isDie = false;

        if (GameDataContainer.Instance.GameData.IsCatSceneView)
            SplineAnimateStartCommand.Execute(transform);
        else
            SplineAnimateEndCommand.Execute();

        ManagerUniRx.AddObjectDisposable(ChangingPosition);
        ManagerUniRx.AddObjectDisposable(DoodleDieCommand);
        ManagerUniRx.AddObjectDisposable(GetStarCommand);
        ManagerUniRx.AddObjectDisposable(SplineAnimateStartCommand);
        ManagerUniRx.AddObjectDisposable(SplineAnimateEndCommand);
    }

    private void FixedUpdate()
    {
        if (_zPosition != (int)_transform.position.z)
        {
            _zPosition = (int)_transform.position.z;
            ChangingPosition.Execute(_zPosition);
        }

        if (_isDie == false && _transform.position.y < -4)
        {
            _isDie = true;
            DoodleDieCommand.Execute();
        }

        if (_splineAnimate.enabled && _splineAnimate.NormalizedTime == 1)
            SplineAnimateEndCommand.Execute();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Star"))
        {
            var starView = other.gameObject.GetComponent<StarView>();
            starView.SetActive(false);

            GetStarCommand.Execute();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_isDie == false && collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            _isDie = true;
            DoodleDieCommand.Execute();
        }
    }

    private void OnValidate()
    {
        if (_rigidbody == null)
            _rigidbody = GetComponent<Rigidbody>();

        if (_capsuleCollider == null)
            _capsuleCollider = GetComponent<CapsuleCollider>();

        if (_splineAnimate == null)
            _splineAnimate = GetComponent<SplineAnimate>();

        if (_animator == null)
            _animator = GetComponent<Animator>();

        var components = GetComponents<BaseComponent>();

        foreach (var component in components)
        {
            if (_components.Contains(component) == false)
                _components.Add(component);
        }
    }

    private void OnDestroy()
    {
        ManagerUniRx.Dispose(ChangingPosition);
        ManagerUniRx.Dispose(DoodleDieCommand);
        ManagerUniRx.Dispose(GetStarCommand);
        ManagerUniRx.Dispose(SplineAnimateStartCommand);
        ManagerUniRx.Dispose(SplineAnimateEndCommand);
    }
}
