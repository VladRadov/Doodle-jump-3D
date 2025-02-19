using System.Collections.Generic;
using UnityEngine;

using UniRx;

public class ManagerEnemies : BaseManager
{
    private EnemyController _enemyController;

    [SerializeField] private List<RootEnemyView> _enemyPrefabs;
    [SerializeField] private float _borderX;
    [SerializeField] private float _yPositionEnemy;

    public EnemyController EnemyController => _enemyController;
    public ReactiveCommand CreateEnemyCommand = new();
    public ReactiveCommand DieEnemyCommand = new();

    public override void Initialize()
    {
        _enemyController = new EnemyController();

        ManagerUniRx.AddObjectDisposable(CreateEnemyCommand);
        ManagerUniRx.AddObjectDisposable(DieEnemyCommand);
    }

    public void SpawnEnemy(Transform parent)
    {
        var index = Random.Range(0, _enemyPrefabs.Count);
        var rootEnemy = PoolObjects<RootEnemyView>.GetObject(_enemyPrefabs[index], parent);
        CreateEnemyCommand.Execute();

        var newPosition = GetRandomPosition();
        rootEnemy.SetLocalPosition(newPosition);

        _enemyController.AddEnemyStorage(rootEnemy.EnemyView);

        rootEnemy.EnemyView.DieEnemyCommand = new();
        rootEnemy.EnemyView.DieEnemyCommand.Subscribe(_ => { DieEnemyCommand.Execute(); });
    }

    private Vector3 GetRandomPosition()
    {
        var x = Random.Range(-_borderX, _borderX);
        return new Vector3(x, _yPositionEnemy, 0);
    }

    private void OnDestroy()
    {
        ManagerUniRx.Dispose(CreateEnemyCommand);
        ManagerUniRx.Dispose(DieEnemyCommand);
    }
}
