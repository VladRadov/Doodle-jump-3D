using UnityEngine;

using DG.Tweening;

public class StarView : MonoBehaviour
{
    private Transform _transform;
    private float _topPositionYMove;
    private float _bottomPositionYMove;

    [SerializeField] private float _durationMoveOneSide;
    [SerializeField] private float _durationRotationOneSide;
    [SerializeField] private float _longMoveOneSide;
    [SerializeField] private Vector3 _angleRotation;

    public void SetActive(bool value)
        => gameObject.SetActive(value);

    private void Start()
    {
        _transform = transform;
        _topPositionYMove = _transform.position.y + _longMoveOneSide;
        _bottomPositionYMove = _transform.position.y - _longMoveOneSide;

        MoveToTop();
    }

    private void MoveToTop()
        => transform.DOMoveY(_topPositionYMove, _durationMoveOneSide).OnComplete(() => { MoveToBottom(); });

    private void MoveToBottom()
        => transform.DOMoveY(_bottomPositionYMove, _durationMoveOneSide).OnComplete(() => { MoveToTop(); });

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Rocket") || other.gameObject.layer == LayerMask.NameToLayer("Star"))
            SetActive(false);
    }

    private void FixedUpdate()
    {
        transform.DORotate(_transform.rotation.eulerAngles + _angleRotation, _durationRotationOneSide);
    }
}
