using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using UniRx;

public class PlatformView : MonoBehaviour
{
    private bool _isDoodleOnPlatform;

    [SerializeField] private Outline _outline;
    [SerializeField] private Color _colorDefault;

    public bool IsDoodleOnPlatform => _isDoodleOnPlatform;
    public Color ColorDefault => _colorDefault;
    public ReactiveCommand<PlatformView> OnCollisionMap = new();

    public void SetActive(bool value)
        => gameObject.SetActive(value);

    public void SetValueDoodleOnPlatform(bool value)
        => _isDoodleOnPlatform = value;

    public void SetLocalPosition(Vector3 position)
        => transform.localPosition = position;

    public void SetActiveOutline(bool value)
        => _outline.enabled = value;

    public void ActiveOutlineColor(Color colorEntry)
    {
        _outline.enabled = true;
        _outline.OutlineColor = colorEntry;
    }

    private void Start()
    {
        ManagerUniRx.AddObjectDisposable(OnCollisionMap);
    }

    private void OnEnable()
    {
        _isDoodleOnPlatform = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        var doodle = collision.gameObject.GetComponent<DoodleView>();

        if (doodle != null)
            _isDoodleOnPlatform = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Map"))
        {
            SetActive(false);
            OnCollisionMap.Execute(this);
        }
    }

    private void OnValidate()
    {
        if (_outline == null)
            _outline = GetComponent<Outline>();
    }

    private void OnDestroy()
    {
        ManagerUniRx.Dispose(OnCollisionMap);
    }
}
