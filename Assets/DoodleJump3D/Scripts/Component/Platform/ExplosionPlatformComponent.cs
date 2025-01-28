using UnityEngine;

using Cysharp.Threading.Tasks;
using UniRx;

public class ExplosionPlatformComponent : BaseComponent
{
    private Color _colorEmission;
    private Color _defaultColorEmission;
    private bool _isStartedExplosion;
    private Material _material;

    [SerializeField] private GameObject _childeCub;
    [SerializeField] private MeshRenderer _meshRenderer;
    [SerializeField] private ExplosionView _prefabExplosion;
    [SerializeField] private float _stepExplosion;
    [SerializeField] private int _delayExplosion;

    public ReactiveCommand ExplodingPlatformCommand;

    public override void Start()
    {
        base.Start();

        _material = transform.GetComponent<Renderer>().material;
        _colorEmission = _material.GetColor("_EmissionColor");
        _defaultColorEmission = _colorEmission;

        ManagerUniRx.AddObjectDisposable(ExplodingPlatformCommand);
    }

    private void OnEnable()
    {
        _isStartedExplosion = false;
        _meshRenderer.enabled = true;
        _childeCub.SetActive(true);
    }

    private async void Explosion()
    {
        _isStartedExplosion = true;

        _colorEmission = _defaultColorEmission;

        while (_colorEmission.g >= 0)
        {
            _material.SetColor("_EmissionColor", _colorEmission - new Color(0, _stepExplosion, 0));
            await UniTask.Delay(_delayExplosion);
            _colorEmission = _material.GetColor("_EmissionColor");
        }

        var explosion = PoolObjects<ExplosionView>.GetObject(_prefabExplosion, transform.position, Quaternion.identity);
        ExplosionEffect(explosion);
        ExplodingPlatformCommand.Execute();
    }

    private async void ExplosionEffect(ExplosionView explosion)
    {
        _meshRenderer.enabled = false;
        _childeCub.SetActive(false);

        await UniTask.Delay(1000);

        explosion.SetActive(false);
        gameObject.SetActive(false);
        _material.SetColor("_EmissionColor", _defaultColorEmission);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_isStartedExplosion == false && collision.gameObject.layer == LayerMask.NameToLayer("Doodle"))
            Explosion();
    }

    private void OnValidate()
    {
        if (_meshRenderer == null)
            _meshRenderer = transform.GetComponent<MeshRenderer>();
    }

    private void OnDestroy()
    {
        ManagerUniRx.Dispose(ExplodingPlatformCommand);
    }
}
