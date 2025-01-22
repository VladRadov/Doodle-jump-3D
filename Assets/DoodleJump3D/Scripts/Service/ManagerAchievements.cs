using System.Collections.Generic;
using UnityEngine;

public class ManagerAchievements : BaseManager
{
    private AchievementsController _achievementsController;

    [SerializeField] private List<Achivement> _achivements;
    [SerializeField] private List<AchivementView> _achivementsItem;

    public AchievementsController Controller => _achievementsController;

    public override void Initialize()
    {
        _achievementsController = new AchievementsController(_achivements, _achivementsItem);
    }

    public void ShowAchivements()
        => _achievementsController?.LoadingNoCompletedAchivements();

    public void OnDisable()
    {
        _achievementsController.Dispose();
    }
}
