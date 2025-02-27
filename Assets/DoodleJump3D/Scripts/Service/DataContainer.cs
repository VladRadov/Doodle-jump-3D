using UnityEngine;

using UniRx;

public class DataContainer<T> : MonoBehaviour
{
    public static T Instance;
    public ReactiveCommand SetInstanceCommand = new();

    public virtual void Initialize()
    {
        
    }

    protected void Initialize(T instance)
    {
        if (Instance == null)
        {
            Instance = instance;
            SetInstanceCommand.Execute();
            transform.parent = null;
            ManagerUniRx.AddObjectDisposable(SetInstanceCommand);

            DontDestroyOnLoad(this.gameObject);
        }
        else
            Destroy(this.gameObject);
    }

    private void OnDestroy()
    {
        ManagerUniRx.Dispose(SetInstanceCommand);
    }
}
