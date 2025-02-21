using UnityEngine;

public class BoxStretcher : MonoBehaviour
{
    private Transform _transform;
    private Transform _transformDoodle;

    [SerializeField] private DoodleView _doodle;
    [SerializeField] private Transform _bodyDoodle;

    [SerializeField] private Vector3 _scaleDown = new Vector3(1.2f, 0.8f, 1.2f);
    [SerializeField] private Vector3 _scaleUp = new Vector3(0.8f, 1.2f, 0.8f);

    [SerializeField] private float _scaleKoefficient;
    [SerializeField] private float _rotationKoefficient;

    public void SetPosition(Vector3 newPosition)
        => transform.position = newPosition;

    private void Start()
    {
        _transform = transform;
        _transformDoodle = _doodle.transform;
    }

    private void Update()
    {
        Vector3 relativePosition = _transformDoodle.InverseTransformPoint(_transform.position);
        float interpolant = relativePosition.y * _scaleKoefficient;
        Vector3 scale = Lerp3(_scaleDown, Vector3.one, _scaleUp, interpolant);
        _bodyDoodle.localScale = scale;
    }

    private Vector3 Lerp3(Vector3 a, Vector3 b, Vector3 c, float t)
        => t > 0 ? Vector3.Lerp(a, b, t + 1f) : Vector3.LerpUnclamped(b, c, t);
}
