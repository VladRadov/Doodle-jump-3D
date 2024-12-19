using UnityEngine;

public class DataSettingsContainer : DataContainer<DataSettingsContainer>
{
    [SerializeField] private Settings _settings;

    public Settings Settings => _settings;

    private void Awake()
    {
        Initialize(this);
    }
}
