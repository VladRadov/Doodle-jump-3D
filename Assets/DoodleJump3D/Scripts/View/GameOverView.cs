using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class GameOverView : MonoBehaviour
{
    [SerializeField] private Text _currentResult;
    [SerializeField] private Text _bestResult;
    [SerializeField] private Button _buttonMenu;

    public ReactiveCommand GameOverCommand = new();
    public ReactiveCommand StartNewGameCommand = new();

    private void Start()
    {
        ManagerUniRx.AddObjectDisposable(GameOverCommand);
        ManagerUniRx.AddObjectDisposable(StartNewGameCommand);

        _buttonMenu.onClick.AddListener(() => { StartNewGameCommand.Execute(); });
    }

    private void OnEnable()
    {
        _currentResult.text = GameDataContainer.Instance.GameData.CurrentResult.ToString();
        _bestResult.text = GameDataContainer.Instance.GameData.BestResult.ToString();
    }

    private void OnDestroy()
    {
        ManagerUniRx.Dispose(GameOverCommand);
        ManagerUniRx.Dispose(StartNewGameCommand);
    }
}
