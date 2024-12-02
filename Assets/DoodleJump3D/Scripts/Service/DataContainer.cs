using UnityEngine;

public class DataContainer : MonoBehaviour
{
    [SerializeField] private Settings _settings;

    public Settings Settings => _settings;
    public static DataContainer Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
}
