using UnityEngine;
using Cysharp.Threading.Tasks;

public class ExplosionPlatformComponent : BaseComponent
{
    private Color _colorEmission;
    private bool _isStartedExplosion;
    private Material _material;

    [SerializeField] private GameObject _childeCub;
    [SerializeField] private MeshRenderer _meshRenderer;
    [SerializeField] private ExplosionView _prefabExplosion;
    [SerializeField] private float _stepExplosion;
    [SerializeField] private int _delayExplosion;

    public override void Start()
    {
        base.Start();
        _material = transform.GetComponent<Renderer>().material;
        _colorEmission = _material.GetColor("_EmissionColor");
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

        while (_colorEmission.g >= 0)
        {
            _material.SetColor("_EmissionColor", _colorEmission - new Color(0, _stepExplosion, 0));
            await UniTask.Delay(_delayExplosion);
            _colorEmission = _material.GetColor("_EmissionColor");
        }

        var explosion = PoolObjects<ExplosionView>.GetObject(_prefabExplosion, transform.position, Quaternion.identity);

        _meshRenderer.enabled = false;
        _childeCub.SetActive(false);
        await UniTask.Delay(1000);
        explosion.SetActive(false);
        gameObject.SetActive(false);
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
}
