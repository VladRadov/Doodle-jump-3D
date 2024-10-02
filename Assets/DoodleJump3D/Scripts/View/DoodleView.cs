using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class DoodleView : MonoBehaviour
{
    private float _zPosition;

    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private CapsuleCollider _capsuleCollider;
    [SerializeField] private List<BaseComponent> _components;

    public List<BaseComponent> Components => _components;
    public ReactiveCommand<Vector3> OnChangePositionDoodle = new();

    private void Start()
    {
        ManagerUniRx.AddObjectDisposable(OnChangePositionDoodle);
        _zPosition = -1;
    }

    private void FixedUpdate()
    {
        if (Mathf.Round(_zPosition) != Mathf.Round(transform.position.z))
        {
            OnChangePositionDoodle.Execute(transform.position);
            _zPosition = transform.position.z;
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
        ManagerUniRx.Dispose(OnChangePositionDoodle);
    }
}
