using UnityEngine;

public class EffectShakeComponent : BaseComponent
{
    [SerializeField] private ParticleSystem _effectShake;

    public void SetActive(bool value)
        => _effectShake.gameObject.SetActive(value);
}
