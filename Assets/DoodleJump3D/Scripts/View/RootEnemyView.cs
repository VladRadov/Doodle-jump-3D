using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootEnemyView : MonoBehaviour
{
    [SerializeField] private EnemyView _enemyView;

    public EnemyView EnemyView => _enemyView;
}
