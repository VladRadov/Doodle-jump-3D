using UnityEngine;

public class BulletView : MonoBehaviour
{
    private float _speed;
    private float _currentTime;

    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private float _timeInvisible;

    public void SetActive(bool value)
        => gameObject.SetActive(value);

    public void SetPosition(Vector3 newPosition)
        => transform.position = newPosition;

    public void StartShot(float speed)
    {
        _speed = speed;
        _currentTime = _timeInvisible;
    }

    private void FixedUpdate()
    {
        if (_currentTime > 0)
            _currentTime -= Time.deltaTime;
        else
            SetActive(false);

        _rigidbody.velocity = Vector3.forward * _speed;
    }

    private void OnValidate()
    {
        if (_rigidbody == null)
            _rigidbody = GetComponent<Rigidbody>();
    }
}
