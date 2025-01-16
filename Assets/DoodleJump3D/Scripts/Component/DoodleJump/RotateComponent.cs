using UnityEngine;

public class RotateComponent : BaseComponent
{
    [SerializeField] private Vector3 _rotateFlyingToRocket;

    public void RotateFlyingToRocket()
        => transform.rotation = Quaternion.Euler(_rotateFlyingToRocket);

    public void ResetRotate()
        => transform.rotation = Quaternion.identity;
}
