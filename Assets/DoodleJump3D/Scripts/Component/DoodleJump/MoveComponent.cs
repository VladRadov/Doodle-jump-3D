using UnityEngine;

public class MoveComponent : BaseComponent
{
    private Rigidbody _rigidbody;

    [SerializeField] private float _speed;

    public void Move(Vector2 force)
    {
        var newForce = new Vector3(force.x, 0, 0);
        _rigidbody.AddForce(newForce * _speed * Time.deltaTime, ForceMode.VelocityChange);
    }

    public void MoveToPosition(Vector3 targetPosition)
    {
        var force = new Vector3(targetPosition.x, 0, targetPosition.z) - new Vector3(transform.position.x, 0, transform.position.z);
        _rigidbody.AddForceAtPosition(force * _speed * Time.deltaTime, targetPosition, ForceMode.VelocityChange);
    }

    private void OnValidate()
    {
        if (_rigidbody == null)
            _rigidbody = GetComponent<Rigidbody>();
    }
}
