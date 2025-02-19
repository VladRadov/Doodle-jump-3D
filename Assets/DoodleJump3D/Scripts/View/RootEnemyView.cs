using UnityEngine;

public class RootEnemyView : MonoBehaviour
{
    [SerializeField] private EnemyView _enemyView;

    public EnemyView EnemyView => _enemyView;

    public void SetLocalPosition(Vector3 position)
        => transform.localPosition = position;
}
