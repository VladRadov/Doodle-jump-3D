using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class ManagerMenu : BaseManager
{
    private AnimationPanel _animationPanel;

    [SerializeField] private MenuView _menuPanel;
    [SerializeField] private SettingsView _settingsView;
    [SerializeField] private AchievementsView _achievementsView;
    [SerializeField] private GameOverView _gameOverView;
    [SerializeField] private Button _play;
    [SerializeField] private Button _settings;
    [SerializeField] private Button _achievements;

    public ReactiveCommand PlayingCommand = new();
    public SettingsView SettingsView => _settingsView;
    public GameOverView GameOverView => _gameOverView;

    public override void Initialize()
    {
        _animationPanel = new AnimationPanel(new Vector2(0, 0), new Vector2(0, 882), 0.3f);

        _play.onClick.AddListener(() =>
        {
            PlayingCommand.Execute();
            _menuPanel.SetActive(false);
        });

        _settings.onClick.AddListener(() =>
        {
            _animationPanel.MoveDOAchorPos(_settingsView.gameObject, _achievementsView.gameObject);
        });

        _achievements.onClick.AddListener(() =>
        {
            _animationPanel.MoveDOAchorPos(_achievementsView.gameObject, _settingsView.gameObject);
        });

        _settingsView.ClosingEventHandler.AddListener(() =>
        {
            _animationPanel.MoveDOAchorPos(null, _settingsView.gameObject);
        });

        _achievementsView.ClosingEventHandler.AddListener(() =>
        {
            _animationPanel.MoveDOAchorPos(null, _achievementsView.gameObject);
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
