using UnityEngine;
using UnityEngine.UI;

using UniRx;
using Cysharp.Threading.Tasks;

public class ManagerMenu : BaseManager
{
    private AnimationPanel _animationPanel;

    [SerializeField] private MenuView _menuPanel;
    [SerializeField] private SettingsView _settingsView;
    [SerializeField] private AchievementsOnMenuView _achievementsView;
    [SerializeField] private GameOverView _gameOverView;
    [SerializeField] private Button _play;
    [SerializeField] private Button _settings;
    [SerializeField] private Button _achievements;

    public ReactiveCommand PlayingCommand = new();
    public SettingsView SettingsView => _settingsView;
    public GameOverView GameOverView => _gameOverView;

    public override void Initialize()
    {
        _animationPanel = new AnimationPanel( 0.3f);

        _play.onClick.AddListener(() =>
        {
            PlayingCommand.Execute();
            _menuPanel.SetActive(false);
        });

        _settings.onClick.AddListener(async () =>
        {
            await _animationPanel.MoveDOAchorPosHideObject(_achievementsView.gameObject, new Vector2(0, 882));
            _animationPanel.MoveDOAchorPosActiveObject(_settingsView.gameObject, new Vector2(0, 0));
        });

        _achievements.onClick.AddListener(async () =>
        {
            await _animationPanel.MoveDOAchorPosHideObject(_settingsView.gameObject, new Vector2(0, 882));
            _animationPanel.MoveDOAchorPosActiveObject(_achievementsView.gameObject, new Vector2(0, 0));
        });

        _settingsView.ClosingEventHandler.AddListener(() =>
        {
            _animationPanel.MoveDOAchorPosHideObject(_settingsView.gameObject, new Vector2(0, 882));
        });

        _achievementsView.ClosingEventHandler.AddListener(() =>
        {
            _animationPanel.MoveDOAchorPosHideObject(_achievementsView.gameObject, new Vector2(0, 882));
        });

        _gameOverView.GameOverCommand.Subscribe(_ =>
        {
            _animationPanel.MoveDOAchorPosActiveObject(_gameOverView.gameObject, new Vector2(0, 0));
        });

        _gameOverView.StartNewGameCommand.Subscribe(async _ =>
        {
            _animationPanel.MoveDOAchorPosHideObject(_gameOverView.gameObject, new Vector2(0, 882));
            await UniTask.Delay(500);
            ManagerScenes.Instance.LoadAsyncFromCoroutine("Game");
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
