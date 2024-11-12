using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotDoodleComponent : BaseComponent
{
    [SerializeField] private BulletView _bulletViewPrefab;
    [SerializeField] private float _speedShot;

    public void Shot()
    {
        var bullet = PoolObjects<BulletView>.GetObject(_bulletViewPrefab, transform.position, Quaternion.identity);
        bullet.StartShot(_speedShot);
    }
}
