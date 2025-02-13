using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

using UniRx;

public class SettingsView : MonoBehaviour
{
    [SerializeField] private Slider _sliderSounds;
    [SerializeField] private Button _close;

    public ReactiveCommand<float> ChangingVolume = new();
    [HideInInspector]
    public UnityEvent ClosingEventHandler;

    public void SetActive(bool value)
        => gameObject.SetActive(value);

    public void ChangeVolume(float volume)
        => ChangingVolume.Execute(volume);

    private void Start()
    {
        _sliderSounds.value = DataSettingsContainer.Instance.Settings.VolumeSounds;
        _sliderSounds.onValueChanged.AddListener((value) => { ChangeVolume(value); });
        _close.onClick.AddListener(() => { ClosingEventHandler?.Invoke(); });
        ManagerUniRx.AddObjectDisposable(ChangingVolume);
    }

    private void OnDestroy()
    {
        ManagerUniRx.Dispose(ChangingVolume);
    }
}
