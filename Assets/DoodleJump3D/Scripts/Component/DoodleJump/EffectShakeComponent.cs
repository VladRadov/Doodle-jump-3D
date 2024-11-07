using UnityEngine;
using UniRx;

public class EffectShakeComponent : BaseComponent
{
    [SerializeField] private ParticleSystem _effectShake;

    public ReactiveCommand DoodleDieCommand = new();

    public void SetActive(bool value)
        => gameObject.SetActive(value);

    public override void Start()
    {
        base.Start();
        ManagerUniRx.AddObjectDisposable(DoodleDieCommand);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            SetActive(true);
    }

    private void OnDestroy()
    {
        ManagerUniRx.Dispose(DoodleDieCommand);
    }
}
