using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerEnemies : BaseManager
{
    private EnemyController _enemyController;

    [SerializeField] private List<EnemyView> _enemyPrefabs;
    [SerializeField] private float _borderX;
    [SerializeField] private float _yPositionEnemy;

    public EnemyController EnemyController => _enemyController;

    public override void Initialize()
    {
        _enemyController = new EnemyController();
    }

    public void SpawnEnemy(Transform parent)
    {
        var index = Random.Range(0, _enemyPrefabs.Count);
        var enemy = PoolObjects<EnemyView>.GetObject(_enemyPrefabs[index], parent);

        var newPosition = GetRandomPosition();
        enemy.SetLocalPosition(newPosition);

        _enemyController.AddEnemyStorage(enemy);
    }

    private Vector3 GetRandomPosition()
    {
        var x = Random.Range(-_borderX, _borderX);
        return new Vector3(x, _yPositionEnemy, 0);
    }
}
