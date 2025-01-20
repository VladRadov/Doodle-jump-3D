using System.Collections.Generic;

using UniRx;
using Cysharp.Threading.Tasks;

public class AchievementsController
{
    private readonly List<Achivement> _achivements;
    private readonly List<AchivementView> _achivementsItem;

    public AchievementsController(List<Achivement> achivements, List<AchivementView> achivementsItem)
    {
        _achivements = achivements;
        _achivementsItem = achivementsItem;

        ManagerUniRx.AddObjectDisposable(FlyRocketCommand);
        ManagerUniRx.AddObjectDisposable(JumpPlatformCommand);
        ManagerUniRx.AddObjectDisposable(KillEnemyCommand);
        ManagerUniRx.AddObjectDisposable(RotateDoodleCommand);
        ManagerUniRx.AddObjectDisposable(DistanceCompletedCommand);
    }

    public ReactiveCommand FlyRocketCommand = new();
    public ReactiveCommand JumpPlatformCommand = new();
    public ReactiveCommand KillEnemyCommand = new();
    public ReactiveCommand RotateDoodleCommand = new();
    public ReactiveCommand DistanceCompletedCommand = new();

    public async void LoadingNoCompletedAchivements()
    {
        var findedNoCompletedAchivements = FindNoCompletedAchivements();

        if (findedNoCompletedAchivements.Count == 0)
            return;

        for (int i = 0; i < _achivementsItem.Count; i++)
        {
            if (i >= findedNoCompletedAchivements.Count)
                return;

            if (findedNoCompletedAchivements[i] is AchivementAstronaut)
                FlyRocketCommand.Subscribe(_ => { findedNoCompletedAchivements[i].IncreaseCountSuccess(); });
            else if(findedNoCompletedAchivements[i] is AchivementEpicFail)
                JumpPlatformCommand.Subscribe(_ => { findedNoCompletedAchivements[i].IncreaseCountSuccess(); });
            else if (findedNoCompletedAchivements[i] is AchivementKillMonsters)
                KillEnemyCommand.Subscribe(_ => { findedNoCompletedAchivements[i].IncreaseCountSuccess(); });
            else if (findedNoCompletedAchivements[i] is AchivementRotate)
                RotateDoodleCommand.Subscribe(_ => { findedNoCompletedAchivements[i].IncreaseCountSuccess(); });
            else if (findedNoCompletedAchivements[i] is AchivementRunner)
                DistanceCompletedCommand.Subscribe(_ => { findedNoCompletedAchivements[i].IncreaseCountSuccess(); });
            else if (findedNoCompletedAchivements[i] is AchivementSharpShooter)
                DistanceCompletedCommand.Subscribe(_ => { findedNoCompletedAchivements[i].IncreaseCountSuccess(); });

            _achivementsItem[i].Initialize(findedNoCompletedAchivements[i]);

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
    }

    private List<Achivement> FindNoCompletedAchivements()
    {
        SortAchivementAscendingLevel();
        List<Achivement> findedAchivements = new();

        foreach (var achivement in _achivements)
        {
            if (achivement.IsAchivementSuccess)
                continue;

            findedAchivements = FindAchivements(achivement.LevelAchivement);

            if (findedAchivements != null)
                break;
        }

        return findedAchivements;
    }

    private List<Achivement> FindAchivements(int levelAchivement)
        => _achivements.FindAll(achivementTemp => achivementTemp.LevelAchivement == levelAchivement);

    private void SortAchivementAscendingLevel()
    {
        _achivements.Sort((Achivement achivement1, Achivement achivement2) =>
        {
            return achivement1.LevelAchivement < achivement2.LevelAchivement ? achivement1.LevelAchivement : achivement2.LevelAchivement;
        });
    }
}
