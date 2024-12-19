using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class DoodleView : MonoBehaviour
{
    private Transform _transform;
    private bool _isDie;

    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private CapsuleCollider _capsuleCollider;
    [SerializeField] private List<BaseComponent> _components;

    public List<BaseComponent> Components => _components;
    public ReactiveCommand<int> ChangingPosition = new();
    public ReactiveCommand DoodleDieCommand = new();

    private void Start()
    {
        _transform = transform;
        _isDie = false;

        ManagerUniRx.AddObjectDisposable(ChangingPosition);
        ManagerUniRx.AddObjectDisposable(DoodleDieCommand);
    }

    private void FixedUpdate()
    {
        ChangingPosition.Execute(((int)_transform.position.z));

        if (_isDie == false && _transform.position.y < -2)
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
