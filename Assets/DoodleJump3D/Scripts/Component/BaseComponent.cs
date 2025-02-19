using UnityEngine;

public class BaseComponent : MonoBehaviour
{
    [Header("���.\\����.")]
    [SerializeField] protected bool _isOnState;

    public bool IsOnState => _isOnState;

    public virtual void Start()
    {
        if (_isOnState == false)
            this.enabled = false;
    }
}
