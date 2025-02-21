using UnityEngine;

using Cysharp.Threading.Tasks;
using UniRx;

public class MoveComponent : BaseComponent
{
    private bool _isMoveToPosition;

    [Header("Components")]
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private DoodleView _doodleView;
    [Header("Settings")]
    [SerializeField] private float _speedMove;
    [SerializeField] private float _speedMoveTransform;
    [SerializeField] private float _maxXSideLeft;
    [SerializeField] private float _maxXSideRight;

    public ReactiveCommand<Vector3> ChangeSideCommand = new();

    public void Move(Vector2 force)
    {
        var isChangingSideOnLeft = _doodleView.BaseTransform.position.x > _maxXSideRight;
        var isChangingSideOnRight = _doodleView.BaseTransform.position.x < _maxXSideLeft;

        if (isChangingSideOnLeft || isChangingSideOnRight)
        {
            var newX = isChangingSideOnLeft ? _maxXSideLeft : _maxXSideRight;
            var newPosition = new Vector3(newX, _doodleView.BaseTransform.position.y, _doodleView.BaseTransform.position.z);
            ChangeSideCommand.Execute(newPosition);
            transform.position = newPosition;
            _doodleView.BoxStretcher.SetPosition(newPosition);
        }

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

        if (_doodleView == null)
            _doodleView = GetComponent<DoodleView>();
    }
}
