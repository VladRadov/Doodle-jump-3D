using UnityEngine;

public class EffectJumpingView : MonoBehaviour
{
    [SerializeField] private ParticleSystem _effectJumping;

    public void Play()
        => _effectJumping?.Play();

    public void SetPosition(Vector3 newPosition)
        => transform.position = newPosition;

    private void FixedUpdate()
    {
        if (_effectJumping.isStopped)
            gameObject.SetActive(false);
    }

    private void OnValidate()
    {
        if (_effectJumping == null)
            _effectJumping = GetComponent<ParticleSystem>();
    }
}
