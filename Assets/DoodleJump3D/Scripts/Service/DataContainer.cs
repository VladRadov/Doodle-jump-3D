using UnityEngine;

using UniRx;

public class DataContainer<T> : MonoBehaviour
{
    public static T Instance;
    public ReactiveCommand SetInstanceCommand = new();

    public void Initialize(T instance)
    {
        DontDestroyOnLoad(this);

        if (Instance == null)
        {
            Instance = instance;
            SetInstanceCommand.Execute();
        }
        else
            Destroy(this);

        ManagerUniRx.AddObjectDisposable(SetInstanceCommand);
    }

    private void OnDisable()
    {
        ManagerUniRx.Dispose(SetInstanceCommand);
    }
}
