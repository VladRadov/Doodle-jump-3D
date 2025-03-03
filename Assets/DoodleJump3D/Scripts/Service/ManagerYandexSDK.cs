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
        YandexGame.GetDataEvent -= OnGetDataEvent;
        YandexGame.GetDataEvent += OnGetDataEvent;

        YandexGame.OpenFullAdEvent -= OnOpenFullAd;
        YandexGame.OpenFullAdEvent += OnOpenFullAd;

        YandexGame.CloseFullAdEvent -= OnCloseFullAd;
        YandexGame.CloseFullAdEvent += OnCloseFullAd;

        ManagerUniRx.AddObjectDisposable(OpenningFullAdCommand);
        ManagerUniRx.AddObjectDisposable(ClosingFullAdCommand);
        ManagerUniRx.AddObjectDisposable(InitializeSdkSuccess);
    }

    public void FullscreenAdsShow()
        => YandexGame.FullscreenShow();

    public void SaveBestScore(long score)
        => YandexGame.NewLeaderboardScores(_nameLeaderboard, score);

    private void OnGetDataEvent()
    {
        if (InitializeSdkSuccess.IsDisposed == false)
            InitializeSdkSuccess.Execute();
    }

    private void OnOpenFullAd()
    {
        if(OpenningFullAdCommand.IsDisposed == false)
            OpenningFullAdCommand.Execute();
    }

    private void OnCloseFullAd()
    {
        if (ClosingFullAdCommand.IsDisposed == false)
            ClosingFullAdCommand.Execute();
    }

    private void OnDestroy()
    {
        ManagerUniRx.Dispose(OpenningFullAdCommand);
        ManagerUniRx.Dispose(ClosingFullAdCommand);
        ManagerUniRx.Dispose(InitializeSdkSuccess);
    }
}
