using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

public class PlatformView : MonoBehaviour
{
    private bool _isDoodleOnPlatform;

    [SerializeField] private Outline _outline;
    [SerializeField] private Color _colorDefault;
    [SerializeField] private Rigidbody _rigidbody;

    public bool IsDoodleOnPlatform => _isDoodleOnPlatform;
    public Color ColorDefault => _colorDefault;
    public ReactiveCommand<PlatformView> OnCollisionTexture = new();
    public ReactiveCommand OnCollisionDoodle = new();

    public void SetActive(bool value)
        => gameObject.SetActive(value);

    public void SetLocalPosition(Vector3 position)
        => transform.localPosition = position;

    public void SetActiveOutline(bool value)
    {
        if(_outline != null)
            _outline.enabled = value;
    }

    public void ActiveOutlineColor(Color colorEntry)
    {
        if (_outline != null)
        {
            _outline.enabled = true;
            _outline.OutlineColor = colorEntry;
        }
    }

    public void OffGravity()
    {
        _rigidbody.constraints = RigidbodyConstraints.FreezePositionY;
        _rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
    }

    public void OnGravity()
    {
        _rigidbody.useGravity = false;
        _rigidbody.constraints = RigidbodyConstraints.None;
        _rigidbody.useGravity = true;
    }

    protected virtual void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Map") || other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            SetActive(false);
            OnCollisionTexture.Execute(this);
        }
    }

    protected virtual void Start()
    {
        ManagerUniRx.AddObjectDisposable(OnCollisionTexture);
        ManagerUniRx.AddObjectDisposable(OnCollisionDoodle);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Doodle"))
        {
            _isDoodleOnPlatform = true;
            OnCollisionDoodle.Execute();
        }
    }

    private void OnEnable()
    {
        _isDoodleOnPlatform = false;
    }

    private void OnValidate()
    {
        if (_outline == null)
            _outline = GetComponent<Outline>();

        if (_rigidbody == null)
            _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnDestroy()
    {
        ManagerUniRx.Dispose(OnCollisionTexture);
        ManagerUniRx.Dispose(OnCollisionDoodle);
    }
}
