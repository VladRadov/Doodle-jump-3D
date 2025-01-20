using System.Collections.Generic;
using UnityEngine;

public class ManagerAchievements : BaseManager
{
    private AchievementsController _achievementsController;

    [SerializeField] private List<Achivement> _achivements;
    [SerializeField] private List<AchivementView> _achivementsItem;

    public override void Initialize()
    {
        _achievementsController = new AchievementsController(_achivements, _achivementsItem);
        _achievementsController.LoadingNoCompletedAchivements();
    }
}
