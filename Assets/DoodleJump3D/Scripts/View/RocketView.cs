using UnityEngine;

public class RocketView : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private FlyRocketComponent _rocketComponent;

    public Rigidbody Rigidbody => _rigidbody;

    public void SetActive(bool value)
        => gameObject.SetActive(value);

    public void SetPosition(Vector3 newPosition)
        => transform.position = newPosition;

    private void OnTriggerEnter(Collider other)
    {
        if (_rocketComponent.IsFlying == false && (other.gameObject.layer == LayerMask.NameToLayer("Rocket") || other.gameObject.layer == LayerMask.NameToLayer("Ground")))
            SetActive(false);
    }

    private void OnValidate()
    {
        if (_rigidbody == null)
            _rigidbody = GetComponent<Rigidbody>();

        if (_rocketComponent == null)
            _rocketComponent = GetComponent<FlyRocketComponent>();
    }
}
