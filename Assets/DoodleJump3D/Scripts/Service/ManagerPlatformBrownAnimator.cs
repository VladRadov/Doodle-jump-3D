using UnityEngine;

public class ManagerPlatformBrownAnimator : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    public void PlayRotation()
        => _animator.SetTrigger("IsRotation");

    public void SetDefaultState()
    {
        _animator.Rebind();
        _animator.ResetTrigger("IsRotation");
    }

    private void OnValidate()
    {
        if (_animator == null)
            _animator = GetComponent<Animator>();
    }
}
