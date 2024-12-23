using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerRocket : BaseManager
{
    [SerializeField] private RocketView _rocketPrefab;

    public override void Initialize()
    {

    }

    public void SpawnRocket(Transform parent)
    {
        var rocket = PoolObjects<RocketView>.GetObject(_rocketPrefab, parent);
        rocket.transform.localPosition = new Vector3(0, 5, 0);
    }
}
