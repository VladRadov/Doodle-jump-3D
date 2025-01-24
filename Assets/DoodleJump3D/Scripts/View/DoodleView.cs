using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class DoodleView : MonoBehaviour
{
    private Transform _transform;
    private bool _isDie;
    private int _zPosition;

    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private CapsuleCollider _capsuleCollider;
    [SerializeField] private List<BaseComponent> _components;
    [SerializeField] private Transform _pointJointRocket;

    public List<BaseComponent> Components => _components;
    public ReactiveCommand<int> ChangingPosition = new();
    public ReactiveCommand DoodleDieCommand = new();
    public bool IsDie => _isDie;
    public Vector3 PointJointRocket => _pointJointRocket.position;

    private void Start()
    {
        _transform = transform;
        _zPosition = (int)_transform.position.z;
        _isDie = false;

        ManagerUniRx.AddObjectDisposable(ChangingPosition);
        ManagerUniRx.AddObjectDisposable(DoodleDieCommand);
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
    }
}
