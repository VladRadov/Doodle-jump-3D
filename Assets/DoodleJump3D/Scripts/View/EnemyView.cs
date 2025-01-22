using UnityEngine;

using UniRx;

public class EnemyView : MonoBehaviour
{
    public ReactiveCommand DieEnemyCommand = new();

    public void SetActive(bool value)
        => gameObject.SetActive(value);

    private void Start()
    {
        ManagerUniRx.AddObjectDisposable(DieEnemyCommand);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Bullet"))
        {
            DieEnemyCommand.Execute();
            SetActive(false);
            collision.gameObject.SetActive(false);
        }
    }

    private void OnDestroy()
    {
        ManagerUniRx.Dispose(DieEnemyCommand);
    }
}
