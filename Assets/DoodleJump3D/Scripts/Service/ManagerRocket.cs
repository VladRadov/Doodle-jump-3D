using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerRocket : BaseManager
{
    [SerializeField] private RocketView _rocketPrefab;

    public override void Initialize()
    {

    }

    public void SpawnRocket(Transform platform)
    {
        var rocket = PoolObjects<RocketView>.GetObject(_rocketPrefab);
        rocket.transform.position = new Vector3(platform.position.x, platform.position.y + 5, platform.position.z);
    }
}
