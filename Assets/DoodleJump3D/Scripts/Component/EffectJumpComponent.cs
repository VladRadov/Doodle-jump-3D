using UnityEngine;

public class EffectJumpComponent : BaseComponent
{
    [SerializeField] private EffectJumpingView _effectJumpingPrefab;

    public void Create(Vector3 positionDoodle)
    {
        var effectJumping = PoolObjects<EffectJumpingView>.GetObject(_effectJumpingPrefab);
        effectJumping.SetPosition(positionDoodle);
        _effectJumpingPrefab?.Play();
    }
}
