using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class ManagerMenu : BaseManager
{
    [SerializeField] private MenuView _menuPanel;
    [SerializeField] private SettingsView _settingsView;
    [SerializeField] private AchievementsView _achievementsView;
    [SerializeField] private Button _play;
    [SerializeField] private Button _settings;
    [SerializeField] private Button _achievements;

    public ReactiveCommand PlayingCommand = new();
    public SettingsView SettingsView => _settingsView;

    public override void Initialize()
    {
        _play.onClick.AddListener(() =>
        {
            PlayingCommand.Execute();
            _menuPanel.SetActive(false);
        });

        _settings.onClick.AddListener(() =>
        {
            _settingsView.SetActive(true);
        });

        _achievements.onClick.AddListener(() =>
        {
            _achievementsView.SetActive(true);
        });
    }

    private void Start()
    {
        ManagerUniRx.AddObjectDisposable(PlayingCommand);
    }

    private void OnDestroy()
    {
        ManagerUniRx.Dispose(PlayingCommand);
    }
}
