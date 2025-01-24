using System;
using System.Collections.Generic;
using System.Linq;

using UniRx;
using Cysharp.Threading.Tasks;

public class AchievementsController
{
    private List<Achievement> _achievements;
    private readonly List<AchievementView> _achievementsItem;
    private List<Achievement> _currentAchievements;

    public AchievementsController(List<Achievement> achievements, List<AchievementView> achievementsItem)
    {
        _achievements = achievements;
        _achievementsItem = achievementsItem;

        ManagerUniRx.AddObjectDisposable(FlyRocketCommand);
        ManagerUniRx.AddObjectDisposable(JumpPlatformCommand);
        ManagerUniRx.AddObjectDisposable(KillEnemyCommand);
        ManagerUniRx.AddObjectDisposable(RotateDoodleCommand);
        ManagerUniRx.AddObjectDisposable(DistanceCompletedCommand);
        ManagerUniRx.AddObjectDisposable(TakeTheStarCommand);
    }

    public ReactiveCommand FlyRocketCommand = new();
    public ReactiveCommand JumpPlatformCommand = new();
    public ReactiveCommand KillEnemyCommand = new();
    public ReactiveCommand RotateDoodleCommand = new();
    public ReactiveCommand DistanceCompletedCommand = new();
    public ReactiveCommand TakeTheStarCommand = new();

    public async void LoadingNoCompletedAchievements()
    {
        if (TryFindNoCompletedAchievements() == false)
            return;

        for (int i = 0; i < _achievementsItem.Count; i++)
        {
            if (i >= _currentAchievements.Count)
                return;
            
            var currentAchievement = _currentAchievements[i];
            var achievementItem = _achievementsItem[i];

            currentAchievement.CheckSuccessAchievement();
            SubcriberSuccessedAchievements(currentAchievement, achievementItem);

            _achievementsItem[i].Initialize(_currentAchievements[i]);

            await UniTask.Delay(500);
        }
    }

    public void Dispose()
    {
        ManagerUniRx.Dispose(FlyRocketCommand);
        ManagerUniRx.Dispose(JumpPlatformCommand);
        ManagerUniRx.Dispose(KillEnemyCommand);
        ManagerUniRx.Dispose(RotateDoodleCommand);
        ManagerUniRx.Dispose(DistanceCompletedCommand);
        ManagerUniRx.Dispose(TakeTheStarCommand);
    }

    private void SubcriberSuccessedAchievements(Achievement currentAchievement, AchievementView achievementItem)
    {
        Action actionOnIncreaseCountSuccessAchievement = () =>
        {
            currentAchievement.IncreaseCountSuccess();
            achievementItem.SetProgressAchievement(currentAchievement.CurrentCountSuccess);

            if(IsCurrentAchievementsComplited())
                ChangeActiveAchievements();
        };

        if (currentAchievement is AchievementAstronaut)
            FlyRocketCommand.Subscribe(_ => { actionOnIncreaseCountSuccessAchievement(); });
        else if (currentAchievement is AchievementEpicFail)
            JumpPlatformCommand.Subscribe(_ => { actionOnIncreaseCountSuccessAchievement(); });
        else if (currentAchievement is AchievementKillMonsters)
            KillEnemyCommand.Subscribe(_ => { actionOnIncreaseCountSuccessAchievement(); });
        else if (currentAchievement is AchievementRotate)
            RotateDoodleCommand.Subscribe(_ => { actionOnIncreaseCountSuccessAchievement(); });
        else if (currentAchievement is AchievementRunner)
            DistanceCompletedCommand.Subscribe(_ => { actionOnIncreaseCountSuccessAchievement(); });
        else if (currentAchievement is AchievementAstronom)
            TakeTheStarCommand.Subscribe(_ => { actionOnIncreaseCountSuccessAchievement(); });
    }

    private bool IsCurrentAchievementsComplited()
    {
        return _currentAchievements
            .Where(achivement => achivement.IsAchievementSuccess == false)
            .Count() == 0 ? true : false;
    }

    private async void ChangeActiveAchievements()
    {
        for (int i = 0; i < _achievementsItem.Count; i++)
        {
            _achievementsItem[i].HideAchivementView();
            await UniTask.Delay(500);
        }

        LoadingNoCompletedAchievements();
    }

    private bool TryFindNoCompletedAchievements()
    {
        SortAchievementAscendingLevel();

        foreach (var achievement in _achievements)
        {
            if (achievement.IsAchievementSuccess)
                continue;

            _currentAchievements = FindAchievements(achievement.LevelAchievement);

            if (_currentAchievements != null)
                return true;
        }

        return false;
    }

    private List<Achievement> FindAchievements(int levelAchievement)
        => _achievements.FindAll(achivementTemp => achivementTemp.LevelAchievement == levelAchievement);

    private void SortAchievementAscendingLevel()
        => _achievements = _achievements.OrderBy(achievement => achievement.LevelAchievement).ToList();
}
