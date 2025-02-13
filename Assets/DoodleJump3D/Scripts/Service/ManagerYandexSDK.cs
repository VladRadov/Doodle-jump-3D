using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using YG;

public class ManagerYandexSDK : BaseManager
{
    [SerializeField] private string _nameLeaderboard;

    public override void Initialize()
    {

    }

    public void FullscreenAdsShow()
        => YandexGame.FullscreenShow();

    public void SaveBestScore(long score)
        => YandexGame.NewLeaderboardScores(_nameLeaderboard, score);
}
