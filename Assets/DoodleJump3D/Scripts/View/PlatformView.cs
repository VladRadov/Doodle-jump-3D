using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class PlatformView : MonoBehaviour
{
    private bool _isDoodleOnPlatform;

    [SerializeField] private Outline _outline;
    [SerializeField] private Color _colorDefault;

    public bool IsDoodleOnPlatform => _isDoodleOnPlatform;
    public Color ColorDefault => _colorDefault;

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

    public async void NoActiveWithDelay()
    {
        await UniTask.Delay(2000);
        SetActive(false);
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

    private void OnValidate()
    {
        if (_outline == null)
            _outline = GetComponent<Outline>();
    }
}
