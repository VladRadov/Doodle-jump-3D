using System.Collections.Generic;
using UnityEngine;

public class ManagerAchievements : BaseManager
{
    private AchievementsController _achievementsController;

    [SerializeField] private List<Achievement> _achievements;
    [SerializeField] private List<AchievementView> _achievementsItem;

    public AchievementsController Controller => _achievementsController;

    public override void Initialize()
    {
        _achievementsController = new AchievementsController(_achievements, _achievementsItem);
    }

    public void ShowAchivements()
        => _achievementsController?.LoadingNoCompletedAchievements();

    public void OnDisable()
    {
        _achievementsController.Dispose();
    }
}
