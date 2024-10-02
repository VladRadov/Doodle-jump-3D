using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveComponent : BaseComponent
{
    private Rigidbody _rigidbody;

    [SerializeField] private float _speed;

    public void Move(Vector2 force)
    {
        var newForce = new Vector3(force.x, 0, 0);
        _rigidbody.AddForce(newForce * _speed, ForceMode.VelocityChange);
    }

    private void OnValidate()
    {
        if (_rigidbody == null)
            _rigidbody = GetComponent<Rigidbody>();
    }
}
