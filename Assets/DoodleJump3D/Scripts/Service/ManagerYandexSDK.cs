using UnityEngine;

using YG;
using UniRx;

public class ManagerYandexSDK : BaseManager
{
    [SerializeField] private string _nameLeaderboard;

    public ReactiveCommand OpenningFullAdCommand = new();
    public ReactiveCommand ClosingFullAdCommand = new();
    public ReactiveCommand InitializeSdkSuccess = new();

    public override void Initialize()
    {
        YandexGame.GetDataEvent += () => { InitializeSdkSuccess.Execute(); };
        YandexGame.OpenFullAdEvent += () => { OpenningFullAdCommand.Execute(); };
        YandexGame.CloseFullAdEvent += () => { ClosingFullAdCommand.Execute(); };

        ManagerUniRx.AddObjectDisposable(OpenningFullAdCommand);
        ManagerUniRx.AddObjectDisposable(ClosingFullAdCommand);
        ManagerUniRx.AddObjectDisposable(InitializeSdkSuccess);
    }

    public void FullscreenAdsShow()
        => YandexGame.FullscreenShow();

    public void SaveBestScore(long score)
        => YandexGame.NewLeaderboardScores(_nameLeaderboard, score);

    private void OnDestroy()
    {
        ManagerUniRx.Dispose(OpenningFullAdCommand);
        ManagerUniRx.Dispose(ClosingFullAdCommand);
        ManagerUniRx.Dispose(InitializeSdkSuccess);
    }
}
