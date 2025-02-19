using UnityEngine;

public class FrameMapView : MonoBehaviour
{
    [SerializeField] private Transform _tail;

    public Transform Tail => _tail;

    public void SetPosition(Vector3 newPosition)
        => transform.position = newPosition;
}
