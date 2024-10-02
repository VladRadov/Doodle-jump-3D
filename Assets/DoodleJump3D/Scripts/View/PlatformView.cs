using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformView : MonoBehaviour
{
    private bool _isDoodleOnPlatform;

    [SerializeField] private Outline _outline;

    public bool IsDoodleOnPlatform => _isDoodleOnPlatform;

    public void SetValueDoodleOnPlatform(bool value)
        => _isDoodleOnPlatform = value;

    public void SetPosition(Vector3 position)
        => transform.position = position;

    public void SetActiveOutline(bool value)
        => _outline.enabled = value;

    public void ActiveOutlineEntry(Color colorEntry)
    {
        _outline.enabled = true;
        _outline.OutlineColor = colorEntry;
    }

    private void Start()
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
