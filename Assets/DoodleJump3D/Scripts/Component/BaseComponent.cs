using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseComponent : MonoBehaviour
{
    [Header("¬кл.\\выкл.")]
    [SerializeField] protected bool _isOnState;

    public bool IsOnState => _isOnState;

    public virtual void Start()
    {
        if (_isOnState == false)
            this.enabled = false;
    }
}
