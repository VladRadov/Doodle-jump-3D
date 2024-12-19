using UnityEngine;

public class DataContainer<T> : MonoBehaviour
{
    public static T Instance;

    public void Initialize(T instance)
    {
        if (Instance == null)
            Instance = instance;
    }
}
