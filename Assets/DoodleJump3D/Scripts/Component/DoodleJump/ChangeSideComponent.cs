using UnityEngine;

using UniRx;

public class ChangeSideComponent : BaseComponent
{
    [Header("Components")]
    [SerializeField] private DoodleView _doodleView;

    public ReactiveCommand<Vector3> ChangeSideCommand = new();

    public override void Start()
    {
        base.Start();

        ManagerUniRx.AddObjectDisposable(ChangeSideCommand);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Side"))
        {
            var sideMapView = other.gameObject.GetComponent<SideMapView>();
            if (sideMapView != null)
            {
                var newPosition = new Vector3(sideMapView.OppositeSide.transform.position.x, _doodleView.BaseTransform.position.y, _doodleView.BaseTransform.position.z);
                ChangeSideCommand.Execute(newPosition);
                transform.position = newPosition;
                _doodleView.BoxStretcher.SetPosition(newPosition);
            }
        }
    }

    private void OnValidate()
    {
        if (_doodleView == null)
            _doodleView = GetComponent<DoodleView>();
    }

    private void OnDestroy()
    {
        ManagerUniRx.Dispose(ChangeSideCommand);
    }
}
