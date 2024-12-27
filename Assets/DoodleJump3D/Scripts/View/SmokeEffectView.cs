using UnityEngine;

public class SmokeEffectView : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particleSystem;

    public void SetPosition(Vector3 position)
        => transform.position = position;

    private void FixedUpdate()
    {
        CheckStopSmokeEffect();
    }

    private void CheckStopSmokeEffect()
    {
        if (_particleSystem.isStopped)
            gameObject.SetActive(false);
    }

    private void OnValidate()
    {
        if (_particleSystem == null)
            _particleSystem = GetComponent<ParticleSystem>();
    }
}
