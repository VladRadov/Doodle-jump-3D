using UnityEngine;

public class SideMapView : MonoBehaviour
{
    [SerializeField] private SideMapView _oppositeSide;

    public SideMapView OppositeSide => _oppositeSide;
}
