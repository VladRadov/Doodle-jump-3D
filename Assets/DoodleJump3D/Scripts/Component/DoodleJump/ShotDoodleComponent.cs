using UnityEngine;

using UniRx;

public class ShotDoodleComponent : BaseComponent
{
    [SerializeField] private BulletView _bulletViewPrefab;
    [SerializeField] private float _speedShot;

    public ReactiveCommand ShotingCommand = new();

    public void Shot()
    {
        var bullet = PoolObjects<BulletView>.GetObject(_bulletViewPrefab, transform.position, Quaternion.identity);
        var positionOffset = new Vector3(0, 0, 4);
        bullet.SetPosition(bullet.transform.position + positionOffset);
        bullet.StartShot(_speedShot);
        ShotingCommand.Execute();
    }

    public override void Start()
    {
        base.Start();
        ManagerUniRx.AddObjectDisposable(ShotingCommand);
    }

    private void OnDestroy()
    {
        ManagerUniRx.Dispose(ShotingCommand);
    }
}
