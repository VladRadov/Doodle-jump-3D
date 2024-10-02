using UnityEngine;

public class EffectJumpComponent : BaseComponent
{
    [SerializeField] private EffectJumpingView _effectJumpingPrefab;

    public void Create(Vector3 positionJump)
    {
        var effectJumping = PoolObjects<EffectJumpingView>.GetObject(_effectJumpingPrefab);
        effectJumping.SetPosition(positionJump);
        _effectJumpingPrefab?.Play();
    }
}
