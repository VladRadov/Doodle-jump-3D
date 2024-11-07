using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController
{
    private List<EnemyView> _enemies;

    public EnemyController()
    {
        _enemies = new List<EnemyView>();
    }

    public void AddEnemyStorage(EnemyView enemy)
    {
        if(_enemies.Contains(enemy) == false)
            _enemies.Add(enemy);
    }

    public void NoActiveOldEnemies(FrameMapView frameMapView)
    {
        for (int i = 0; i < _enemies.Count; i++)
        {
            var isEnemyOnFrameMap = _enemies[i].transform.parent == frameMapView.transform;

            if (isEnemyOnFrameMap)
                _enemies[i].SetActive(false);
        }
    }
}
