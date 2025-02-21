using UnityEngine;

using Cysharp.Threading.Tasks;

public class MoveComponent : BaseComponent
{
    private bool _isMoveToPosition;

    [Header("Components")]
    [SerializeField] private Rigidbody _rigidbody;
    [Header("Settings")]
    [SerializeField] private float _speedMove;
    [SerializeField] private float _speedMoveTransform;

    public void Move(Vector2 force)
    {
        var newForce = new Vector3(force.x, 0, 0);
        _rigidbody.AddForce(newForce * _speedMove * Time.deltaTime, ForceMode.VelocityChange);
    }

    public async void MoveToPosition(Vector3 targetPosition)
    {
        _isMoveToPosition = true;

        var targetPositionNoY = new Vector3(targetPosition.x, transform.position.y, targetPosition.z);

        while (_isMoveToPosition)
        {
            targetPositionNoY = new Vector3(targetPosition.x, transform.position.y, targetPosition.z);
            var newPosition = Vector3.Lerp(transform.position, targetPositionNoY, _speedMoveTransform * Time.deltaTime);
            _rigidbody.MovePosition(newPosition);

            await UniTask.Delay(10);
        }
    }

    public void EndMoveToPosition()
    {
        if(_isMoveToPosition)
            _isMoveToPosition = false;
    }

    private void OnValidate()
    {
        if (_rigidbody == null)
            _rigidbody = GetComponent<Rigidbody>();
    }
}
